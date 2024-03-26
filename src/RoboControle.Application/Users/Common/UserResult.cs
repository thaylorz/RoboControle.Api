using RoboControle.Domain.Entities;

namespace RoboControle.Application.Users.Common;
public record UserResult(Ulid UserId, string Email, string FirstName, string LastName)
{
    public static UserResult FromUser(UserEntity user)
    {
        return new UserResult(user.Id, user.Email, user.FirstName, user.LastName);
    }
}
