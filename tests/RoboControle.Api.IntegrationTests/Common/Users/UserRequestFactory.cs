using Bogus;

using RoboControle.Contracts;

namespace RoboControle.Api.IntegrationTests.Common.Users;
public static class UserRequestFactory
{
    public static Ulid CreateGetUserRequest(Ulid? userId = null)
    {
        return userId ?? Constants.User.Id;
    }

    public static RegisterRequest CreateRegisterRequest(
        string firstName = Constants.User.FirstName,
        string lastName = Constants.User.LastName,
        string email = Constants.User.Email,
        string password = Constants.User.Password)
    {
        return new(firstName, lastName, email, password);
    }

    public static RegisterRequest CreateRandomRegisterRequest()
    {
        return new Faker<RegisterRequest>()
            .CustomInstantiator(f =>
            new RegisterRequest(
                f.Person.FirstName,
                f.Person.LastName,
                f.Person.Email,
                f.Internet.Password()));
    }

    public static LoginRequest CreateLoginRequest(
        string email = Constants.User.Email,
        string password = Constants.User.Password)
    {
        return new(email, password);
    }

    public static LoginRequest CreateRandomLoginRequest()
    {
        return new Faker<LoginRequest>().CustomInstantiator(f => new LoginRequest(f.Person.Email, f.Internet.Password()));
    }
}
