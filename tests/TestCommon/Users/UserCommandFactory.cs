using RoboControle.Application.Users.Commands.CreateUser;

using TestCommon.TestConstants;

namespace TestCommon.Users;
public static class UserCommandFactory
{
    public static CreateUserCommand CreateUserCommand(
        string firstName = Constants.User.FirstName,
        string lastName = Constants.User.LastName,
        string email = Constants.User.Email,
        string password = Constants.User.Password)
    {
        return new CreateUserCommand(
            firstName,
            lastName,
            email,
            password);
    }
}
