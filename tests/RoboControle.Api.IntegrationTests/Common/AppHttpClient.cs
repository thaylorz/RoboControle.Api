using System.Net.Http.Headers;

using RoboControle.Api.IntegrationTests.Common.Robots;
using RoboControle.Api.IntegrationTests.Common.Users;
using RoboControle.Contracts;
using RoboControle.Contracts.Robot;

namespace RoboControle.Api.IntegrationTests.Common;
public sealed class AppHttpClient(HttpClient _httpClient)
{
    #region Users
    public async Task<HttpResponseMessage> Register(RegisterRequest? registerRequest = null)
    {
        registerRequest ??= UserRequestFactory.CreateRegisterRequest();
        return await _httpClient.PostAsJsonAsync("users/register", registerRequest);
    }

    public async Task<HttpResponseMessage> Login(LoginRequest? loginRequest = null)
    {
        loginRequest ??= UserRequestFactory.CreateLoginRequest();
        return await _httpClient.PostAsJsonAsync("users/login", loginRequest);
    }

    public async Task<HttpResponseMessage> GetUser(string userId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await _httpClient.GetAsync($"users/{userId}");
    }

    #endregion

    #region Robot

    public async Task<HttpResponseMessage> CreateRobot(string token, CreateRobotRequest? createRobotRequest = null)
    {
        createRobotRequest ??= RobotRequestFactory.CreateCreateRobotRequest();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await _httpClient.PostAsJsonAsync("robots", createRobotRequest);
    }

    public async Task<HttpResponseMessage> DeleteRobot(string robotId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await _httpClient.DeleteAsync($"robots/{robotId}");
    }

    public async Task<HttpResponseMessage> GetRobot(string robotId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await _httpClient.GetAsync($"robots/{robotId}");
    }

    public async Task<HttpResponseMessage> ChangeHeadInclination(string robotId, int inclination, string token)
    {
        var changeHeadInclinationRequest = RobotRequestFactory.CreateChangeHeadInclinationRequest(inclination);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await _httpClient.PutAsJsonAsync($"robots/{robotId}/ChangeHeadInclination", changeHeadInclinationRequest);
    }

    public async Task<HttpResponseMessage> ChangeHeadRotation(string robotId, int rotation, string token)
    {
        var changeHeadRotationRequest = RobotRequestFactory.CreateChangeHeadRotationRequest(rotation);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await _httpClient.PutAsJsonAsync($"robots/{robotId}/ChangeHeadRotation", changeHeadRotationRequest);
    }

    public async Task<HttpResponseMessage> ChangeElbowRotation(string robotId, int rotation, string token)
    {
        var changeElbowRotationRequest = RobotRequestFactory.CreateChangeElbowRotationRequest(rotation);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await _httpClient.PutAsJsonAsync($"robots/{robotId}/ChangeElbowRotation", changeElbowRotationRequest);
    }

    public async Task<HttpResponseMessage> ChangeWristRotation(string robotId, int rotation, string token)
    {
        var changeWristRotationRequest = RobotRequestFactory.CreateChangeWristRotationRequest(rotation);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await _httpClient.PutAsJsonAsync($"robots/{robotId}/ChangeWristRotation", changeWristRotationRequest);
    }

    #endregion
}
