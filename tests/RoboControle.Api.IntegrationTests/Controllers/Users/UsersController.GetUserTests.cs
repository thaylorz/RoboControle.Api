using Microsoft.AspNetCore.Http;

using RoboControle.Api.IntegrationTests.Common.Users;
using RoboControle.Api.IntegrationTests.Common.WebApplicationFactory;
using RoboControle.Contracts;

namespace RoboControle.Api.IntegrationTests.Controllers.Users;
[Collection(WebAppFactoryCollection.CollectionName)]
public class GetUser
{
    private readonly AppHttpClient _appHttpClient;

    public GetUser(WebAppFactory webAppFactory)
    {
        _appHttpClient = webAppFactory.CreateAppHttpClient();
        webAppFactory.ResetDatabase();
    }

    [Fact]
    public async Task GetUser_WhenUserExists_ShouldReturnUser()
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();

        await _appHttpClient.Register(registerRequest);

        var loginRequest = UserRequestFactory.CreateLoginRequest(registerRequest.Email, registerRequest.Password);
        var httpResponseMessageLoginResponse = await _appHttpClient.Login(loginRequest);
        var loginResponse = await httpResponseMessageLoginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        // Act
        var response = await _appHttpClient.GetUser(loginResponse!.UserId, loginResponse!.Token);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var user = await response.Content.ReadFromJsonAsync<UserResponse>();
        user.Should().NotBeNull();
        user!.UserId.Should().Be(loginResponse.UserId);
        user!.FirstName.Should().Be(registerRequest.FirstName);
        user!.LastName.Should().Be(registerRequest.LastName);
        user!.Email.Should().Be(registerRequest.Email);
    }

    [Fact]
    public async Task GetUser_WhenUserDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();

        await _appHttpClient.Register(registerRequest);

        var loginRequest = UserRequestFactory.CreateLoginRequest(registerRequest.Email, registerRequest.Password);
        var httpResponseMessageLoginResponse = await _appHttpClient.Login(loginRequest);
        var loginResponse = await httpResponseMessageLoginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        var userId = Ulid.NewUlid().ToString();

        // Act
        var response = await _appHttpClient.GetUser(userId, loginResponse!.Token);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("invalid-token")]
    [InlineData("")]
    public async Task GetUser_WhenTokenIsInvalid_ShouldReturnUnauthorized(string token)
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();

        await _appHttpClient.Register(registerRequest);

        var loginRequest = UserRequestFactory.CreateLoginRequest(registerRequest.Email, registerRequest.Password);
        var httpResponseMessageLoginResponse = await _appHttpClient.Login(loginRequest);
        var loginResponse = await httpResponseMessageLoginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        // Act
        var response = await _appHttpClient.GetUser(loginResponse!.UserId, token);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
