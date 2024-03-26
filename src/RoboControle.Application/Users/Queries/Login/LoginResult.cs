using RoboControle.Domain.Entities;

namespace RoboControle.Application.Users.Queries.Login;
public sealed record LoginResult(Ulid UserId, string FirstName, string LastName, string Email, string Token)
{
    public static LoginResult FromUser(UserEntity user, string token) => new(user.Id, user.FirstName, user.LastName, user.Email, token);
}
