namespace RoboControle.Contracts;
public sealed record LoginResponse(string UserId, string FirstName, string LastName, string Email, string Token);
