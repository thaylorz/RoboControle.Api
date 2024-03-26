using RoboControle.Application.Users.Queries.GetUser;
using RoboControle.Application.Users.Queries.Login;

using TestCommon.TestConstants;

namespace TestCommon.Users;
public static class UserQueryFactory
{
    public static GetUserQuery CreateGetUserQuery(Ulid? userId = null)
    {
        return new GetUserQuery(userId ?? Constants.User.Id);
    }

    public static LoginQuery CreateLoginQuery(string email = Constants.User.Email, string password = Constants.User.Password)
    {
        return new LoginQuery(email, password);
    }
}
