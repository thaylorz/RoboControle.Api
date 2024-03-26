using Microsoft.EntityFrameworkCore;

using RoboControle.Domain.Entities;
using RoboControle.Infrastructure.Common;

namespace RoboControle.Infrastructure.Users.Persistence;
public class UsersRepository(AppDbContext _dbContext) : IUsersRepository
{
    public async Task AddAsync(UserEntity user, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public async Task<UserEntity?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FindAsync(userId, cancellationToken);
    }

    public async Task<UserEntity?> GetByIdAsync(Ulid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FindAsync(userId);
    }

    public async Task RemoveAsync(UserEntity user, CancellationToken cancellationToken)
    {
        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}