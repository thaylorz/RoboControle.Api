using RoboControle.Application.Robots.Common;
using RoboControle.Application.Security.CurrentUserProvider;
using RoboControle.Domain.Entities.Robot;

namespace RoboControle.Application.Robots.Queries.ListRobots;
public sealed class ListRobotsQueryHandler(IRobotsRepository _robotsRepository, ICurrentUserProvider _currentUserProvider) : IRequestHandler<ListRobotsQuery, ErrorOr<List<RobotResult>>>
{
    public async Task<ErrorOr<List<RobotResult>>> Handle(ListRobotsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        var robots = await _robotsRepository.ListByUserIdAsync(currentUser.Id, cancellationToken);
        return robots.Select(RobotResult.FromRobot).ToList();
    }
}
