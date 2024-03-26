using RoboControle.Api.IntegrationTests.Common.Robots;
using RoboControle.Api.IntegrationTests.Common.Users;
using RoboControle.Api.IntegrationTests.Common.WebApplicationFactory;
using RoboControle.Contracts;
using RoboControle.Contracts.Robot;

namespace RoboControle.Api.IntegrationTests.Controllers.Robots;
[Collection(WebAppFactoryCollection.CollectionName)]
public sealed class ChangeHeadInclination
{
    private readonly AppHttpClient _appHttpClient;

    public ChangeHeadInclination(WebAppFactory webAppFactory)
    {
        _appHttpClient = webAppFactory.CreateAppHttpClient();
        webAppFactory.ResetDatabase();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async Task ChangeHeadInclination_ValidRequest_ShouldReturnNoContent(int inclination)
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
        var httpResponseMessageChangeHeadInclinationResponse = await _appHttpClient.ChangeHeadInclination(createResponse!.RobotId, inclination, loginResponse!.Token);
        var response = await _appHttpClient.GetRobot(createResponse!.RobotId, loginResponse!.Token);
        var robot = await response.Content.ReadFromJsonAsync<RobotResponse>();

        // Assert
        httpResponseMessageChangeHeadInclinationResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        robot.Should().NotBeNull();
        robot!.HeadInclination.Should().Be(inclination);
    }

    [Fact]
    public async Task ChangeHeadInclination_InvalidToken_ShouldReturnUnauthorized()
    {
        // Arrange

        // Act
        var response = await _appHttpClient.ChangeHeadInclination(Ulid.Empty.ToString(), Constants.Robot.Inclination, "invalid_token");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Theory]
    [InlineData(6)]
    [InlineData(0)]
    public async Task ChangeHeadInclination_WhenInclinationIsInvalid_ShouldReturnBadRequest(int inclination)
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
        var response = await _appHttpClient.ChangeHeadInclination(createResponse!.RobotId, inclination, loginResponse!.Token);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ChangeHeadInclination_WhenRobotIdIsInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        var registerRequest = UserRequestFactory.CreateRandomRegisterRequest();
        await _appHttpClient.Register(registerRequest);

        var loginRequest = UserRequestFactory.CreateLoginRequest(registerRequest.Email, registerRequest.Password);
        var httpResponseMessageLoginResponse = await _appHttpClient.Login(loginRequest);
        var loginResponse = await httpResponseMessageLoginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        // Act
        var response = await _appHttpClient.ChangeHeadInclination(Ulid.Empty.ToString(), Constants.Robot.Inclination, loginResponse!.Token);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
