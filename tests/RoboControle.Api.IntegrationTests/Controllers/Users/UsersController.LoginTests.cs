using Bogus;

using RoboControle.Api.IntegrationTests.Common.Users;
using RoboControle.Api.IntegrationTests.Common.WebApplicationFactory;
using RoboControle.Contracts;

namespace RoboControle.Api.IntegrationTests.Controllers.Users;
[Collection(WebAppFactoryCollection.CollectionName)]
public sealed class LoginTests
{
    private readonly AppHttpClient _appHttpClient;

    public LoginTests(WebAppFactory webAppFactory)
    {
        _appHttpClient = webAppFactory.CreateAppHttpClient();
        webAppFactory.ResetDatabase();
    }

    [Fact]
    public async Task Login_WhenUserExists_ShouldReturnLoginResponse()
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();
        await _appHttpClient.Register(registerRequest);

        // Act
        var loginRequest = UserRequestFactory.CreateLoginRequest(registerRequest.Email, registerRequest.Password);
        var response = await _appHttpClient.Login(loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
        loginResponse.Should().NotBeNull();
        loginResponse!.UserId.Should().NotBeEmpty();
        loginResponse!.Token.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Login_WhenUserDoesNotExist_ShouldReturnForbidden()
    {
        // Arrange
        var loginRequest = UserRequestFactory.CreateRandomLoginRequest();

        // Act
        var response = await _appHttpClient.Login(loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Login_WhenPasswordIsIncorrect_ShouldReturnForbidden()
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();

        await _appHttpClient.Register(registerRequest);

        var loginRequest = UserRequestFactory.CreateLoginRequest(registerRequest.Email, "wrong-password");

        // Act
        var response = await _appHttpClient.Login(loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Login_WhenEmailIsMissing_ShouldReturnBadRequest()
    {
        // Arrange
        var loginRequest = new Faker<LoginRequest>().CustomInstantiator(f => new LoginRequest(null!, f.Internet.Password()));

        // Act
        var response = await _appHttpClient.Login(loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Login_WhenPasswordIsMissing_ShouldReturnBadRequest()
    {
        // Arrange
        var loginRequest = new Faker<LoginRequest>().CustomInstantiator(f => new LoginRequest(f.Person.Email, null!));

        // Act
        var response = await _appHttpClient.Login(loginRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
