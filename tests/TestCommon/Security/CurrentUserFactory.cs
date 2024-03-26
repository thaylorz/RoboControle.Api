using RoboControle.Application.Security.CurrentUserProvider;

using TestCommon.TestConstants;

namespace TestCommon.Security;

public static class CurrentUserFactory
{
    public static CurrentUser CreateCurrentUser(
        Ulid? id = null,
        string firstName = Constants.User.FirstName,
        string lastName = Constants.User.LastName,
        string email = Constants.User.Email)
    {
        return new CurrentUser(
            id ?? Constants.User.Id,
            firstName,
            lastName,
            email);
    }
}