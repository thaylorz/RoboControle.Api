using RoboControle.Application.Users.Common;
using RoboControle.Domain.Entities;

namespace RoboControle.Application.Users.Queries.GetUser;
public sealed class GetUserQueryHandler(IUsersRepository _usersRepository) : IRequestHandler<GetUserQuery, ErrorOr<UserResult>>
{
    public async Task<ErrorOr<UserResult>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await _usersRepository.GetByIdAsync(request.UserId, cancellationToken) is UserEntity user
            ? UserResult.FromUser(user)
            : Error.NotFound(description: "Usuário não encontrado.");
    }
}
