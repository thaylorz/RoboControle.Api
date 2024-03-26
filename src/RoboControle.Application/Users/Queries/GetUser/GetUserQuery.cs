using RoboControle.Application.Users.Common;

namespace RoboControle.Application.Users.Queries.GetUser;
public record GetUserQuery(Ulid UserId) : IRequest<ErrorOr<UserResult>>;
