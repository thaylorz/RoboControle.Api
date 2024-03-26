using RoboControle.Application.Security;
using RoboControle.Application.Users.Common;
using RoboControle.Domain.Entities;

namespace RoboControle.Application.Users.Commands.CreateUser;
public sealed class CreateUserCommandHandler(IUsersRepository _usersRepository) : IRequestHandler<CreateUserCommand, ErrorOr<UserResult>>
{
    private readonly string _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper") ?? string.Empty;
    private readonly int _iteration = 3;

    public async Task<ErrorOr<UserResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _usersRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (existingUser is not null)
        {
            return Error.Conflict(description: "Já existe usuário com este endereço de email.");
        }

        var passwordSalt = PasswordHasher.GenerateSalt();
        var passwordHash = PasswordHasher.ComputeHash(request.Password, passwordSalt, _pepper, _iteration);

        var userResult = UserEntity.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordSalt,
            passwordHash);

        if (userResult.IsError)
        {
            return userResult.Errors;
        }

        var user = userResult.Value;

        await _usersRepository.AddAsync(user, cancellationToken);

        return UserResult.FromUser(user);
    }
}
