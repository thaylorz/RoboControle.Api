using RoboControle.Api.IntegrationTests.Common.Robots;
using RoboControle.Api.IntegrationTests.Common.Users;
using RoboControle.Api.IntegrationTests.Common.WebApplicationFactory;
using RoboControle.Contracts;
using RoboControle.Contracts.Robot;

namespace RoboControle.Api.IntegrationTests.Controllers.Robots;
[Collection(WebAppFactoryCollection.CollectionName)]
public sealed class CreateRobot
{
    private readonly AppHttpClient _appHttpClient;

    public CreateRobot(WebAppFactory webAppFactory)
    {
        _appHttpClient = webAppFactory.CreateAppHttpClient();
        webAppFactory.ResetDatabase();
    }

    [Fact]
    public async Task CreateRobot_ValidRequest_ShouldReturnRobots()
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();
        await _appHttpClient.Register(registerRequest);

        var loginRequest = UserRequestFactory.CreateLoginRequest(registerRequest.Email, registerRequest.Password);
        var httpResponseMessageLoginResponse = await _appHttpClient.Login(loginRequest);
        var loginResponse = await httpResponseMessageLoginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        var createRobotRequest = RobotRequestFactory.CreateCreateRobotRequest();

        // Act
        var httpResponseMessageCreateResponse = await _appHttpClient.CreateRobot(loginResponse!.Token, createRobotRequest);
        var createResponse = await httpResponseMessageCreateResponse.Content.ReadFromJsonAsync<RobotResponse>();

        // Assert
        httpResponseMessageCreateResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createResponse.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateRobot_InvalidToken_ShouldReturnUnauthorized()
    {
        // Arrange
        var createRobotRequest = RobotRequestFactory.CreateCreateRobotRequest();

        // Act
        var response = await _appHttpClient.CreateRobot(Ulid.Empty.ToString(), createRobotRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateRobot_WhenNameIsMissing_ShouldReturnBadRequest()
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();
        await _appHttpClient.Register(registerRequest);

        var loginRequest = UserRequestFactory.CreateLoginRequest(registerRequest.Email, registerRequest.Password);
        var httpResponseMessageLoginResponse = await _appHttpClient.Login(loginRequest);
        var loginResponse = await httpResponseMessageLoginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        var createRobotRequest = RobotRequestFactory.CreateCreateRobotRequest(string.Empty);

        // Act
        var response = await _appHttpClient.CreateRobot(loginResponse!.Token, createRobotRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
