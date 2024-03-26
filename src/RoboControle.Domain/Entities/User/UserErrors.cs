using ErrorOr;

namespace RoboControle.Domain.Entities;
public static class UserErrors
{
    public static Error InvalidFirstName { get; } = Error.Validation(code: InvalidFirstName.ToString(), description: "Primeiro nome inválido.");
    public static Error InvalidLastName { get; } = Error.Validation(code: InvalidLastName.ToString(), description: "Último nome inválido.");
    public static Error InvalidEmail { get; } = Error.Validation(code: InvalidEmail.ToString(), description: "Email inválido.");
}
