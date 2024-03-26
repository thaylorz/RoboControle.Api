using Microsoft.EntityFrameworkCore;

using RoboControle.Domain.Entities.Robot;
using RoboControle.Infrastructure.Common;

namespace RoboControle.Infrastructure.Robots.Persistence;
public class RobotsRepository(AppDbContext _dbContext) : IRobotsRepository
{
    public async Task AddAsync(RobotEntity robot, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(robot, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<RobotEntity?> GetByIdAsync(Ulid robotId, CancellationToken cancellationToken)
    {
        return await _dbContext.Robots.FindAsync(robotId, cancellationToken);
    }

    public async Task<RobotEntity?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Robots.FirstOrDefaultAsync(robot => robot.Name == name, cancellationToken);
    }

    public async Task<List<RobotEntity>> ListByUserIdAsync(Ulid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Robots
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task RemoveAsync(RobotEntity robot, CancellationToken cancellationToken)
    {
        _dbContext.Remove(robot);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RobotEntity robot, CancellationToken cancellationToken)
    {
        _dbContext.Update(robot);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}