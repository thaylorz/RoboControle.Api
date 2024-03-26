using RoboControle.Application.Users.Common;
using RoboControle.Domain.Entities;

using TestCommon.TestConstants;
using TestCommon.TestUtilities;

namespace TestCommon.Users;

public static class UserFactory
{
    public static UserEntity CreateUser(
        string firstName = Constants.User.FirstName,
        string lastName = Constants.User.LastName,
        string email = Constants.User.Email,
        string password = Constants.User.Password)
    {
        string salt = PasswordUtility.GenerateSalt();
        string hash = PasswordUtility.GenerateHash(password, salt);

        return UserEntity.Create(
            firstName,
            lastName,
            email,
            salt,
            hash).Value;
    }

    public static UserResult CreateUserResult(Ulid? userId = null)
    {
        return new UserResult(
            userId ?? Constants.User.Id,
            Constants.User.FirstName,
            Constants.User.LastName,
            Constants.User.Email);
    }
}