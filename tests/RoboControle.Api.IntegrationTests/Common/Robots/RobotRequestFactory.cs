using Bogus;

using RoboControle.Contracts.Robot;

namespace RoboControle.Api.IntegrationTests.Common.Robots;
public sealed class RobotRequestFactory
{
    public static Ulid CreateGetRobotRequest(Ulid? robotId = null)
    {
        return robotId ?? Constants.Robot.Id;
    }

    public static CreateRobotRequest CreateCreateRobotRequest(string name = Constants.Robot.Name)
    {
        return new(name);
    }

    public static CreateRobotRequest CreateRandomRobotRequest()
    {
        return new Faker<CreateRobotRequest>().CustomInstantiator(f => new CreateRobotRequest(f.Person.Avatar));
    }

    public static ChangeHeadInclinationRequest CreateChangeHeadInclinationRequest(int inclination = Constants.Robot.Inclination)
    {
        return new(inclination);
    }

    public static ChangeHeadInclinationRequest CreateRandomChangeHeadInclinationRequest()
    {
        return new Faker<ChangeHeadInclinationRequest>().CustomInstantiator(f => new ChangeHeadInclinationRequest(f.Random.Int(min: 1, max: 3)));
    }

    public static ChangeHeadRotationRequest CreateChangeHeadRotationRequest(int rotation = Constants.Robot.Rotation)
    {
        return new(rotation);
    }

    public static ChangeHeadRotationRequest CreateRandomChangeHeadRotationRequest()
    {
        return new Faker<ChangeHeadRotationRequest>().CustomInstantiator(f => new ChangeHeadRotationRequest(f.Random.Int(min: 1, max: 5)));
    }

    public static ChangeElbowRotationRequest CreateChangeElbowRotationRequest(int rotation = Constants.Robot.Rotation, int side = Constants.Robot.Side)
    {
        return new(rotation, side);
    }

    public static ChangeElbowRotationRequest CreateRandomChangeElbowRotationRequest()
    {
        return new Faker<ChangeElbowRotationRequest>().CustomInstantiator(f => new ChangeElbowRotationRequest(f.Random.Int(min: 1, max: 4), f.Random.Int(1, 2)));
    }

    public static ChangeWristRotationRequest CreateChangeWristRotationRequest(int rotation = Constants.Robot.Rotation, int side = Constants.Robot.Side)
    {
        return new(rotation, side);
    }

    public static ChangeWristRotationRequest CreateRandomChangeWristRotationRequest()
    {
        return new Faker<ChangeWristRotationRequest>().CustomInstantiator(f => new ChangeWristRotationRequest(f.Random.Int(min: 1, max: 7), f.Random.Int(1, 2)));
    }
}
