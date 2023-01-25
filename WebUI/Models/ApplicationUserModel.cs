using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebUI.Models.Enum;

namespace WebUI.Models;

public class ApplicationUserModel : IdentityUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
    public DateTime LastSeenDate { get; set; } = DateTime.UtcNow;
    public Status CurrentStatus { get; set; } = Status.Active;
}
