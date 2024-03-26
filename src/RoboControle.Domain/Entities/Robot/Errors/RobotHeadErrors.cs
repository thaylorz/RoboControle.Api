using ErrorOr;

namespace RoboControle.Domain.Common;
public static class RobotHeadErrors
{
    public static Error InvalidInclinationState { get; } = Error.Validation(code: InvalidInclinationState.ToString(), description: "Estado de inclinação inválido.");
    public static Error InvalidRotationState { get; } = Error.Validation(code: InvalidRotationState.ToString(), description: "Estado de rotação inválido.");
    public static Error InvalidRotationWhileInclinedDown { get; } = Error.Validation(code: InvalidRotationWhileInclinedDown.ToString(), description: "Não é possível rotacionar a cabeça enquanto ela estiver inclinada para baixo.");
}
