namespace RoboControle.Application.Security.CurrentUserProvider;

public record CurrentUser(
    Ulid Id,
    string FirstName,
    string LastName,
    string Email);