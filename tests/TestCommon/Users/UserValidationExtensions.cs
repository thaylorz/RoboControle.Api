using RoboControle.Application.Users.Commands.CreateUser;
using RoboControle.Application.Users.Common;

namespace TestCommon.Users;
public static class UserValidationExtensions
{
    public static void AssertCreatedFrom(this UserResult userResult, CreateUserCommand command)
    {
        userResult.FirstName.Should().Be(command.FirstName);
        userResult.LastName.Should().Be(command.LastName);
        userResult.Email.Should().Be(command.Email);
    }
}
