using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RoboControle.Application.Common.Interfaces;
using RoboControle.Application.Security.CurrentUserProvider;
using RoboControle.Domain.Entities;
using RoboControle.Domain.Entities.Robot;
using RoboControle.Infrastructure.Common;
using RoboControle.Infrastructure.Robots.Persistence;
using RoboControle.Infrastructure.Security.CurrentUserProvider;
using RoboControle.Infrastructure.Security.TokenGenerator;
using RoboControle.Infrastructure.Security.TokenValidation;
using RoboControle.Infrastructure.Services;
using RoboControle.Infrastructure.Users.Persistence;

namespace RoboControle.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddServices()
            .AddAuthentication(configuration)
            .AddAuthorization()
            .AddPersistence();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddScoped<DbContext, AppDbContext>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>((options) => options.UseInMemoryDatabase("InMemoryDb"));

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRobotsRepository, RobotsRepository>();


        return services;
    }

    private static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services
            .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        return services;
    }
}