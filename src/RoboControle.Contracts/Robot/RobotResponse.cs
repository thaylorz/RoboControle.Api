namespace RoboControle.Contracts.Robot;
public record RobotResponse(
    string RobotId,
    string UserId,
    string Name,
    int HeadInclination,
    int HeadRotation,
    int RightElbowRotation,
    int LeftElbowRotation,
    int RigthWristRotation,
    int LeftWristRotation);