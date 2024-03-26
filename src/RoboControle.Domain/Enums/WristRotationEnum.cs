using System.ComponentModel;

namespace RoboControle.Domain.Enums;
public enum WristRotationEnum
{
    [Description("Rotação para -90º")]
    NegativeRotation90 = 1,

    [Description("Rotação para -45º")]
    NegativeRotation45 = 2,

    [Description("Em repouso")]
    Rest = 3,

    [Description("Rotação para 45º")]
    Rotarion45 = 4,

    [Description("Rotação para 90º")]
    Rotarion90 = 5,

    [Description("Rotação para 135º")]
    Rotarion135 = 6,

    [Description("Rotação para 180º")]
    Rotarion180 = 7,
}
