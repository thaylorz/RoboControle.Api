using ErrorOr;

namespace RoboControle.Domain.Common;
public static class RobotArmErrors
{
    public static Error InvalidWristRotationWhileElbowIsNotStronglyContracted { get; } = Error.Validation(code: InvalidWristRotationWhileElbowIsNotStronglyContracted.ToString(), description: "Não é possível rotacionar o pulso enquanto o cotovelo não estiver fortemente contraído.");
}
