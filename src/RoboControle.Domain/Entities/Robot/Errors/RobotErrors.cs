using ErrorOr;

namespace RoboControle.Domain.Entities.Robot.Errors;
public static class RobotErrors
{
    public static Error InvalidName { get; } = Error.Validation(code: InvalidName.ToString(), description: "Nome inválido.");
    public static Error InvalidUserId { get; } = Error.Validation(code: InvalidUserId.ToString(), description: "Usuário inválido.");
}
