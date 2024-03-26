using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RoboControle.Application.Robots.Commands.ChangeElbowRotation;
using RoboControle.Application.Robots.Commands.ChangeRobotHeadInclination;
using RoboControle.Application.Robots.Commands.ChangeRobotWristRotation;
using RoboControle.Application.Robots.Commands.CreateRobot;
using RoboControle.Application.Robots.Commands.DeleteRobot;
using RoboControle.Application.Robots.Common;
using RoboControle.Application.Robots.Queries.GetRobot;
using RoboControle.Application.Robots.Queries.ListRobots;
using RoboControle.Contracts.Robot;
using RoboControle.Domain.Enums;


namespace RoboControle.Api.Controllers;

[Route("robots")]
public class RobotsController(ISender _mediator) : ApiController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ListRobots()
    {
        var query = new ListRobotsQuery();
        var result = await _mediator.Send(query);

        return result.Match(
            robots => Ok(robots.ConvertAll(ToDto)),
            Problem);
    }

    [HttpGet("{robotId}")]
    [Authorize]
    public async Task<IActionResult> GetRobot(Ulid robotId)
    {
        var query = new GetRobotQuery(robotId);
        var result = await _mediator.Send(query);

        return result.Match(
            robot => Ok(ToDto(robot)),
            Problem);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateRobot(CreateRobotRequest request)
    {
        var command = new CreateRobotCommand(request.Name);
        var result = await _mediator.Send(command);

        return result.Match(createRobotResult => CreatedAtAction(
            actionName: nameof(GetRobot),
            routeValues: new { RobotId = createRobotResult.Id },
            value: ToDto(createRobotResult)), Problem);
    }

    [HttpPut("{robotId}/ChangeHeadInclination")]
    [Authorize]
    public async Task<IActionResult> ChangeHeadInclination(Ulid robotId, [FromBody] ChangeHeadInclinationRequest request)
    {
        var command = new ChangeRobotHeadInclinationCommand(robotId, (HeadInclinationEnum)request.Inclination);
        var result = await _mediator.Send(command);

        return result.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpPut("{robotId}/ChangeHeadRotation")]
    [Authorize]
    public async Task<IActionResult> ChangeHeadRotation(Ulid robotId, [FromBody] ChangeHeadRotationRequest request)
    {
        var command = new ChangeRobotHeadRotationCommand(robotId, (HeadRotationEnum)request.Rotation);
        var result = await _mediator.Send(command);

        return result.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpPut("{robotId}/ChangeElbowRotation")]
    [Authorize]
    public async Task<IActionResult> ChangeElbowRotation(Ulid robotId, [FromBody] ChangeElbowRotationRequest request)
    {
        var command = new ChangeElbowRotationCommand(robotId, (ElbowRotationEnum)request.Rotation, (SideEnum)request.Side);
        var result = await _mediator.Send(command);

        return result.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpPut("{robotId}/ChangeWristRotation")]
    [Authorize]
    public async Task<IActionResult> ChangeWristRotation(Ulid robotId, [FromBody] ChangeWristRotationRequest request)
    {
        var command = new ChangeRobotWristRotationCommand(robotId, (WristRotationEnum)request.Rotation, (SideEnum)request.Side);
        var result = await _mediator.Send(command);

        return result.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpDelete("{robotId}")]
    [Authorize]
    public async Task<IActionResult> DeleteRobot(Ulid robotId)
    {
        var command = new DeleteRobotCommand(robotId);
        var result = await _mediator.Send(command);

        return result.Match(
            _ => NoContent(),
            Problem);
    }

    private static RobotResponse ToDto(RobotResult robotResult)
    {
        return new RobotResponse(
            robotResult.Id.ToString(),
            robotResult.UserId.ToString(),
            robotResult.Name,
            (int)robotResult.HeadInclination,
            (int)robotResult.HeadRotation,
            (int)robotResult.RightElbowRotation,
            (int)robotResult.LeftElbowRotation,
            (int)robotResult.RigthWristRotation,
            (int)robotResult.LeftWristRotation);
    }
}
