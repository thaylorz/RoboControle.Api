namespace RoboControle.Application.Common.Interfaces;
public interface IJwtTokenGenerator
{
    string GenerateToken(Ulid id, string firstName, string lastName, string email);
}