namespace RoboControle.Domain.Entities;
public interface IUsersRepository
{
    Task AddAsync(UserEntity user, CancellationToken cancellationToken);
    Task<UserEntity?> GetByIdAsync(Ulid userId, CancellationToken cancellationToken);
    Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task RemoveAsync(UserEntity user, CancellationToken cancellationToken);
    Task UpdateAsync(UserEntity user, CancellationToken cancellationToken);
}