using RoboControle.Api.IntegrationTests.Common.Robots;
using RoboControle.Api.IntegrationTests.Common.Users;
using RoboControle.Api.IntegrationTests.Common.WebApplicationFactory;
using RoboControle.Contracts;
using RoboControle.Contracts.Robot;

namespace RoboControle.Api.IntegrationTests.Controllers.Robots;
[Collection(WebAppFactoryCollection.CollectionName)]
public sealed class ChangeHeadRotation
{
    private readonly AppHttpClient _appHttpClient;

    public ChangeHeadRotation(WebAppFactory webAppFactory)
    {
        _appHttpClient = webAppFactory.CreateAppHttpClient();
        webAppFactory.ResetDatabase();
    }

    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    public async Task ChangeHeadRotation_ValidRequest_ShouldReturnNoContent(int rotation)
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
        var httpResponseMessageChangeHeadRotationResponse = await _appHttpClient.ChangeHeadRotation(createResponse!.RobotId, rotation, loginResponse!.Token);
        var response = await _appHttpClient.GetRobot(createResponse!.RobotId, loginResponse!.Token);
        var robot = await response.Content.ReadFromJsonAsync<RobotResponse>();

        // Assert
        httpResponseMessageChangeHeadRotationResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        robot.Should().NotBeNull();
        robot!.HeadRotation.Should().Be(rotation);
    }
}
