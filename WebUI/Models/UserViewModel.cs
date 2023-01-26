namespace WebUI.Models;

public class UserViewModel
{
    public IEnumerable<UserModel> Users { get; set; }

    public List<string> CheckedUsers { get; set; }
}
