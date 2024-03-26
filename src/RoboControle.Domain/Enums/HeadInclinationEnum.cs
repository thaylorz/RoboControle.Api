using System.ComponentModel;

namespace RoboControle.Domain.Enums;
public enum HeadInclinationEnum
{
    [Description("Para cima")]
    Up = 1,

    [Description("Em repouso")]
    Rest = 2,

    [Description("Para baixo")]
    Down = 3,
}
