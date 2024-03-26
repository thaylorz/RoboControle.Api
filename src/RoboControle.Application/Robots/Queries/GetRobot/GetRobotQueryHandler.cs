using RoboControle.Application.Robots.Common;
using RoboControle.Domain.Entities.Robot;

namespace RoboControle.Application.Robots.Queries.GetRobot;
public sealed class GetRobotQueryHandler(IRobotsRepository _robotsRepository) : IRequestHandler<GetRobotQuery, ErrorOr<RobotResult>>
{
    public async Task<ErrorOr<RobotResult>> Handle(GetRobotQuery request, CancellationToken cancellationToken)
    {
        return await _robotsRepository.GetByIdAsync(request.RobotId, cancellationToken) is RobotEntity robot
            ? RobotResult.FromRobot(robot)
            : Error.NotFound(description: "Robô não encontrado.");
    }
}
