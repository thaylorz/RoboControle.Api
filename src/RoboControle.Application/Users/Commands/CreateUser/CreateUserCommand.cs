using RoboControle.Application.Users.Common;

namespace RoboControle.Application.Users.Commands.CreateUser;
public record CreateUserCommand(string FirstName, string LastName, string Email, string Password) : IRequest<ErrorOr<UserResult>>;
