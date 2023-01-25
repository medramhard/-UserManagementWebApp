using System.ComponentModel;

namespace WebUI.Models.Enum;

public static class EnumExtensions
{
    public static string GetDescription(this Status value)
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])value
           .GetType()
           .GetField(value.ToString())
           .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }
}
