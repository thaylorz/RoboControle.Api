using ErrorOr;

namespace RoboControle.Domain.Common;
public static class RobotElbowErrors
{
    public static Error InvalidRotationState { get; } = Error.Validation(code: InvalidRotationState.ToString(), description: "Estado de rotação inválido.");
}
