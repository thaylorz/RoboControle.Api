using Microsoft.EntityFrameworkCore;

using RoboControle.Infrastructure.Common;

namespace RoboControle.Api.IntegrationTests.Common.WebApplicationFactory;
public class InMemoryTestDatabase : IDisposable
{
    private readonly DbContextOptions<AppDbContext> _options;

    public InMemoryTestDatabase()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        InitializeDatabase();
    }

    public void InitializeDatabase()
    {
        using var context = new AppDbContext(_options);
        context.Database.EnsureCreated();
    }

    public void ResetDatabase()
    {
        using var context = new AppDbContext(_options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        using var context = new AppDbContext(_options);
        context.Database.EnsureDeleted();
    }
}