using Microsoft.AspNetCore.Rewrite;

namespace RoboControle.Application.Users.Commands.CreateUser;
public sealed class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidation()
    {
        RuleFor(x => x.FirstName)
            .MinimumLength(2)
            .MaximumLength(10000);

        RuleFor(x => x.LastName)
            .MinimumLength(2)
            .MaximumLength(10000);

        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Password)
            .MinimumLength(4)
            .MaximumLength(100);
    }
}
