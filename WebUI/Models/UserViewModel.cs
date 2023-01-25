using WebUI.Models.Enum;

namespace WebUI.Models;

public class UserViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime RegisteredDate { get; set; }
    public DateTime LastSeenDate { get; set; }
    public string CurrentStatus { get; set; }
    public bool Selected { get; set; } = false;

    public static implicit operator UserViewModel(ApplicationUserModel user)
    {
        return new UserViewModel
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
