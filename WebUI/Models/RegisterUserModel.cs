using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models;

public class RegisterUserModel
{
    [Required, DisplayName("First Name")]
    public string FirstName { get; set; }

    [Required, DisplayName("Last Name")]
    public string LastName { get; set; }

    [Required, EmailAddress, DisplayName("Email Address")]
    public string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }

    [Required, DataType(DataType.Password), Compare("Password"), DisplayName("Confirm Password")]
    public string ConfirmPassword { get; set; }
}
