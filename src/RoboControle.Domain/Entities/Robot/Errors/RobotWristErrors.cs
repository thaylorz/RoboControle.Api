using ErrorOr;

namespace RoboControle.Domain.Common;
public static class RobotWristErrors
{
    public static Error InvalidRotationState { get; } = Error.Validation(code: InvalidRotationState.ToString(), description: "Estado de rotação inválido.");
}
