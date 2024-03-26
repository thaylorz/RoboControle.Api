namespace RoboControle.Application.Users.Queries.Login;
public sealed record LoginQuery(string Email, string Password) : IRequest<ErrorOr<LoginResult>>;
