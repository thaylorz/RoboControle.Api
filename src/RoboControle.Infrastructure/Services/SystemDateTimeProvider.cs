using RoboControle.Application.Common.Interfaces;

namespace RoboControle.Infrastructure.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
