using System.Globalization;

using Microsoft.AspNetCore.Localization;

using RoboControle.Api;
using RoboControle.Api.Extensions;
using RoboControle.Application;
using RoboControle.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddApiDoc();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
    });
}

var app = builder.Build();
{
    app.UseExceptionHandler();

    app.UseSwagger();
    app.UseSwaggerUI();

    var defaultCultureInfo = new[] { new CultureInfo("pt-BR") };
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("pt-BR"),
        SupportedCultures = defaultCultureInfo,
        SupportedUICultures = defaultCultureInfo,
    });

    app.UseCors("AllowSpecificOrigin");

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}