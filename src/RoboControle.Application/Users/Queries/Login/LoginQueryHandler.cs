using RoboControle.Application.Common.Interfaces;
using RoboControle.Application.Security;
using RoboControle.Domain.Entities;

namespace RoboControle.Application.Users.Queries.Login;
public sealed class LoginQueryHandler(IUsersRepository _usersRepository, IJwtTokenGenerator _jwtTokenGenerator) : IRequestHandler<LoginQuery, ErrorOr<LoginResult>>
{
    private readonly string _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper") ?? string.Empty;
    private readonly int _iteration = 3;

    public async Task<ErrorOr<LoginResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Error.Unauthorized(description: "Email ou senha invalido.");
        }

        var passwordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, _pepper, _iteration);

        if (user.PasswordHash != passwordHash)
        {
            return Error.Unauthorized(description: "Email ou senha invalido.");
        }

        var token = _jwtTokenGenerator.GenerateToken(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email);

        return LoginResult.FromUser(user, token);
    }
}
