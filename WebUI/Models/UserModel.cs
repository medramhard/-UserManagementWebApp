using System.ComponentModel;
using WebUI.Models.Enum;

namespace WebUI.Models;

public class UserModel
{
    public int Id { get; set; }

    [DisplayName("Full Name")]
    public string FullName { get; set; }

    [DisplayName("Email Address")]
    public string Email { get; set; }

    [DisplayName("Registered")]
    public DateTime RegisteredDate { get; set; }

    [DisplayName("Last Seen")]
    public DateTime LastSeenDate { get; set; }

    [DisplayName("Status")]
    public string CurrentStatus { get; set; }

    public bool Selected { get; set; } = false;

    public static implicit operator UserModel(ApplicationUserModel user)
    {
        return new UserModel
        {
            Id = user.OrderId,
            FullName = $"{ user.FirstName } { user.LastName }",
            Email= user.Email,
            RegisteredDate = user.RegisteredDate,
            LastSeenDate = user.LastSeenDate,
            CurrentStatus = user.CurrentStatus.GetDescription()
        };
    }
}
