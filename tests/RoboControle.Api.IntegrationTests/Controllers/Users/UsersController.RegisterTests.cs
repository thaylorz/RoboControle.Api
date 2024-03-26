using Bogus;

using RoboControle.Api.IntegrationTests.Common.Users;
using RoboControle.Api.IntegrationTests.Common.WebApplicationFactory;
using RoboControle.Contracts;

namespace RoboControle.Api.IntegrationTests.Controllers.Users;
[Collection(WebAppFactoryCollection.CollectionName)]
public sealed class RegisterTests
{
    private readonly AppHttpClient _appHttpClient;

    public RegisterTests(WebAppFactory webAppFactory)
    {
        _appHttpClient = webAppFactory.CreateAppHttpClient();
        webAppFactory.ResetDatabase();
    }

    [Fact]
    public async Task Register_ValidRequest_ShouldReturnCreated()
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();

        // Act
        var response = await _appHttpClient.Register(registerRequest);
        var userResponse = await response.Content.ReadFromJsonAsync<UserResponse>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        Assert.NotNull(response.Headers.Location);
        userResponse.Should().NotBeNull();
    }

    [Fact]
    public async Task Register_WhenUserAlreadyExists_ShouldReturnConflict()
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();
        var response = await _appHttpClient.Register(registerRequest);

        // Act
        var response2 = await _appHttpClient.Register(registerRequest);

        // Assert
        response2.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Register_WhenFirstNameIsMissing_ShouldReturnBadRequest()
    {
        // Arrange
        var registerRequest = new Faker<RegisterRequest>()
            .CustomInstantiator(f =>
                new RegisterRequest(
                    null!,
                    f.Person.LastName,
                    f.Person.Email,
                    f.Internet.Password()));

        // Act
        var response = await _appHttpClient.Register(registerRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_WhenLastNameIsMissing_ShouldReturnBadRequest()
    {
        // Arrange
        var registerRequest = new Faker<RegisterRequest>()
            .CustomInstantiator(f =>
                new RegisterRequest(
                    f.Person.FirstName,
                    null!,
                    f.Person.Email,
                    f.Internet.Password()));

        // Act
        var response = await _appHttpClient.Register(registerRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_WhenEmailIsMissing_ShouldReturnBadRequest()
    {
        // Arrange
        var registerRequest = new Faker<RegisterRequest>()
            .CustomInstantiator(f =>
                new RegisterRequest(
                    f.Person.FirstName,
                    f.Person.LastName,
                    null!,
                    f.Internet.Password()));

        // Act
        var response = await _appHttpClient.Register(registerRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_WhenPasswordIsMissing_ShouldReturnBadRequest()
    {
        // Arrange
        var registerRequest = new Faker<RegisterRequest>()
            .CustomInstantiator(f =>
                new RegisterRequest(f.Person.FirstName, f.Person.LastName, f.Person.Email, null!));

        // Act
        var response = await _appHttpClient.Register(registerRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_WhenPasswordIsTooShort_ShouldReturnBadRequest()
    {
        // Arrange
        var registerRequest = new Faker<RegisterRequest>()
            .CustomInstantiator(f =>
                new RegisterRequest(
                    f.Person.FirstName,
                    f.Person.LastName,
                    f.Person.Email,
                    f.Internet.Password(3)));

        // Act
        var response = await _appHttpClient.Register(registerRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_WhenPasswordIsTooLong_ShouldReturnBadRequest()
    {
        // Arrange
        var registerRequest = new Faker<RegisterRequest>()
            .CustomInstantiator(f =>
                new RegisterRequest(
                    f.Person.FirstName,
                    f.Person.LastName,
                    f.Person.Email,
                    f.Internet.Password(101)));

        // Act
        var response = await _appHttpClient.Register(registerRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_WhenEmailIsInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        var registerRequest = new Faker<RegisterRequest>()
            .CustomInstantiator(f =>
                new RegisterRequest(
                    f.Person.FirstName,
                    f.Person.LastName,
                    f.Person.Email.Replace("@", ""),
                    f.Internet.Password()));

        // Act
        var response = await _appHttpClient.Register(registerRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
