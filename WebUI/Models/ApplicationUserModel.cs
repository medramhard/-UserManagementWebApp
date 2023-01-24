using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models;

public class ApplicationUserModel : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
    public DateTime LastSeenDate { get; set; } = DateTime.UtcNow;
    public Status CurrentStatus { get; set; } = Status.Active;
}
