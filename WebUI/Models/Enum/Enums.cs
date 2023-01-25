using System.ComponentModel;

namespace WebUI.Models.Enum;

public enum Status
{
    [Description("Active")]
    Active = 1,
    [Description("Blocked")]
    Blocked = 2
}
