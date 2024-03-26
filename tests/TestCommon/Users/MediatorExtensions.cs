using RoboControle.Application.Users.Commands.CreateUser;
using RoboControle.Application.Users.Common;
using RoboControle.Application.Users.Queries.GetUser;
using RoboControle.Application.Users.Queries.Login;

namespace TestCommon.Users;
public static class MediatorExtensions
{
    public static async Task<UserResult> CreateUserAsync(this IMediator mediator, CreateUserCommand? command = null)
    {
        command ??= UserCommandFactory.CreateUserCommand();
        var result = await mediator.Send(command);
        result.IsError.Should().BeFalse();
        result.Value.AssertCreatedFrom(command);
        return result.Value;
    }

    public static async Task<UserResult> GetUserAsync(this IMediator mediator, GetUserQuery? query = null)
    {
        query ??= UserQueryFactory.CreateGetUserQuery();
        var result = await mediator.Send(query);
        result.IsError.Should().BeFalse();
        return result.Value;
    }

    public static async Task<LoginResult> LoginAsync(this IMediator mediator, LoginQuery? command = null)
    {
        command ??= UserQueryFactory.CreateLoginQuery();
        var result = await mediator.Send(command);
        result.IsError.Should().BeFalse();
        return result.Value;
    }
}
