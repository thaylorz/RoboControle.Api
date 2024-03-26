using RoboControle.Api.IntegrationTests.Common.Robots;
using RoboControle.Api.IntegrationTests.Common.Users;
using RoboControle.Api.IntegrationTests.Common.WebApplicationFactory;
using RoboControle.Contracts;
using RoboControle.Contracts.Robot;

namespace RoboControle.Api.IntegrationTests.Controllers.Robots;
[Collection(WebAppFactoryCollection.CollectionName)]
public sealed class GetRobot
{
    private readonly AppHttpClient _appHttpClient;

    public GetRobot(WebAppFactory webAppFactory)
    {
        _appHttpClient = webAppFactory.CreateAppHttpClient();
        webAppFactory.ResetDatabase();
    }

    [Fact]
    public async Task GetRobot_WhenRobotExist_ShouldReturnRobots()
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();

        await _appHttpClient.Register(registerRequest);

        var loginRequest = UserRequestFactory.CreateLoginRequest(registerRequest.Email, registerRequest.Password);
        var httpResponseMessageLoginResponse = await _appHttpClient.Login(loginRequest);
        var loginResponse = await httpResponseMessageLoginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        var createRobotRequest = RobotRequestFactory.CreateCreateRobotRequest();
        var httpResponseMessageCreateResponse = await _appHttpClient.CreateRobot(loginResponse!.Token, createRobotRequest);
        var createResponse = await httpResponseMessageCreateResponse.Content.ReadFromJsonAsync<RobotResponse>();

        // Act
        var response = await _appHttpClient.GetRobot(createResponse!.RobotId, loginResponse!.Token);
        var robots = await response.Content.ReadFromJsonAsync<RobotResponse>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        robots.Should().NotBeNull();
    }

    [Fact]
    public async Task GetRobots_WhenRobotsDoNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();

        await _appHttpClient.Register(registerRequest);

        var loginRequest = UserRequestFactory.CreateLoginRequest(registerRequest.Email, registerRequest.Password);
        var httpResponseMessageLoginResponse = await _appHttpClient.Login(loginRequest);
        var loginResponse = await httpResponseMessageLoginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        // Act
        var response = await _appHttpClient.GetRobot(Ulid.Empty.ToString(), loginResponse!.Token);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
