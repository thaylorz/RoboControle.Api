using System.ComponentModel;

namespace RoboControle.Domain.Enums;
public enum HeadRotationEnum
{
    [Description("Rotação para -90º")]
    NegativeRotation90 = 1,

    [Description("Rotação para -45º")]
    NegativeRotation45 = 2,

    [Description("Em repouso")]
    Rest = 3,

    [Description("Rotação para 45º")]
    Rotation45 = 4,

    [Description("Rotação para 90º")]
    Rotation90 = 5,
}
