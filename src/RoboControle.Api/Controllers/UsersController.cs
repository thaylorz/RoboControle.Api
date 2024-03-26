using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RoboControle.Application.Users.Commands.CreateUser;
using RoboControle.Application.Users.Common;
using RoboControle.Application.Users.Queries.GetUser;
using RoboControle.Application.Users.Queries.Login;
using RoboControle.Contracts;

namespace RoboControle.Api.Controllers;

[Route("users")]
public class UsersController(ISender _mediator) : ApiController
{
    [HttpGet("{userId}")]
    [Authorize]
    public async Task<IActionResult> GetUser(Ulid userId)
    {
        var query = new GetUserQuery(userId);
        var result = await _mediator.Send(query);

        return result.Match(
            user => Ok(ToDto(user)),
            Problem);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new CreateUserCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var result = await _mediator.Send(command);

        return result.Match(createUserResult => CreatedAtAction(
            actionName: nameof(GetUser),
            routeValues: new { createUserResult.UserId },
            value: ToDto(createUserResult)), Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var result = await _mediator.Send(query);

        return result.Match(
            user => Ok(ToDto(user)),
            Problem);
    }

    private static UserResponse ToDto(UserResult userResult)
    {
        return new UserResponse(
            userResult.UserId.ToString(),
            userResult.FirstName,
            userResult.LastName,
            userResult.Email);
    }

    private static LoginResponse ToDto(LoginResult user)
    {
        return new LoginResponse(
            user.UserId.ToString(),
            user.FirstName,
            user.LastName,
            user.Email,
            user.Token);
    }
}