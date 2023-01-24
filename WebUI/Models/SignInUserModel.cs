using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models;

public class SignInUserModel
{
    [Required, EmailAddress, DisplayName("Email Address")]
    public string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
}
