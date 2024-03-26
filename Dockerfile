FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/RoboControle.Api/RoboControle.Api.csproj", "RoboControle.Api/"]
COPY ["src/RoboControle.Application/RoboControle.Application.csproj", "RoboControle.Application/"]
COPY ["src/RoboControle.Domain/RoboControle.Domain.csproj", "RoboControle.Domain/"]
COPY ["src/RoboControle.Contracts/RoboControle.Contracts.csproj", "RoboControle.Contracts/"]
COPY ["src/RoboControle.Infrastructure/RoboControle.Infrastructure.csproj", "RoboControle.Infrastructure/"]
COPY ["Directory.Packages.props", "./"]
COPY ["Directory.Build.props", "./"]
RUN dotnet restore "RoboControle.Api/RoboControle.Api.csproj"
COPY . ../
WORKDIR /src/RoboControle.Api
RUN dotnet build "RoboControle.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoboControle.Api.dll"]