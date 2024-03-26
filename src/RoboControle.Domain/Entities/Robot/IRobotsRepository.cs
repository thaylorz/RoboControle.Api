namespace RoboControle.Domain.Entities.Robot;
public interface IRobotsRepository
{
    Task AddAsync(RobotEntity robot, CancellationToken cancellationToken);
    Task<RobotEntity?> GetByIdAsync(Ulid robotId, CancellationToken cancellationToken);
    Task<RobotEntity?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<List<RobotEntity>> ListByUserIdAsync(Ulid userId, CancellationToken cancellationToken);
    Task RemoveAsync(RobotEntity robot, CancellationToken cancellationToken);
    Task UpdateAsync(RobotEntity robot, CancellationToken cancellationToken);
}
