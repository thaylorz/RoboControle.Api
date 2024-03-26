using System.ComponentModel;

namespace RoboControle.Domain.Enums;
public enum ElbowRotationEnum
{
    [Description("Em repouso")]
    Rest = 1,

    [Description("Levemente contraído")]
    SlightlyContracted = 2,

    [Description("Contraído")]
    Contracted = 3,

    [Description("Fortemente contraído")]
    StronglyContract = 4,
}

