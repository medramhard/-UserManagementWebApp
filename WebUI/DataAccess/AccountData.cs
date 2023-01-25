using Microsoft.AspNetCore.Identity;
using WebUI.Data;
using WebUI.Models;
using WebUI.Models.Enum;

namespace WebUI.DataAccess;

public class AccountData : IAccountData
{
    private readonly UserManager<ApplicationUserModel> _userManager;
    private readonly SignInManager<ApplicationUserModel> _signInManager;
    private readonly ApplicationDbContext _db;

    public AccountData(UserManager<ApplicationUserModel> userManager, SignInManager<ApplicationUserModel> signInManager, ApplicationDbContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
    }

    public async Task<IdentityResult> RegisterUser(RegisterUserModel registerUser)
    {
        var user = new ApplicationUserModel()
        {
            Email = registerUser.Email,
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            UserName = registerUser.Email,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, registerUser.Password);
        if (result.Succeeded)
        {
            await _signInManager.PasswordSignInAsync(registerUser.Email, registerUser.Password, true, false);
        }
        return result;
    }

    public async Task<SignInResult> SignInUser(SignInUserModel signInUser)
    {
        var result = await _signInManager.PasswordSignInAsync(signInUser.Email, signInUser.Password, true, false);
        if (result.Succeeded)
        {
            await UpdateLastSeen(signInUser);
        }
        return result;
    }

    private async Task UpdateLastSeen(SignInUserModel signInUser)
    {
        var user = await _userManager.FindByEmailAsync(signInUser.Email);
        user.LastSeenDate= DateTime.UtcNow;
        _db.Users.Attach(user);
        _db.Entry(user).Property(p => p.LastSeenDate).IsModified = true;
        _db.SaveChanges();
    }

    public async Task LogOutUser()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task UpdateUsersStatus(IEnumerable<UserViewModel> users, Status status, bool isBlocked)
    {
        var usernames = GetUsernames(users);
        foreach (var username in usernames)
        {
            var user = await _userManager.FindByEmailAsync(username);
            UpdateCurrentStatus(user, status);
            await _userManager.SetLockoutEnabledAsync(user, isBlocked);
        }
    }

    private void UpdateCurrentStatus(ApplicationUserModel user, Status newStatus)
    {
        user.CurrentStatus = newStatus;
        _db.Users.Attach(user);
        _db.Entry(user).Property(p => p.CurrentStatus).IsModified = true;
        _db.SaveChanges();
    }

    private IEnumerable<string> GetUsernames(IEnumerable<UserViewModel> users)
    {
        return (from user in users where user.Selected select user.Email).ToList();
    }

    public async Task DeleteUsers(IEnumerable<UserViewModel> users)
    {
        var usernames = GetUsernames(users);
        foreach (var username in usernames)
        {
            var user = await _userManager.FindByEmailAsync(username);
            await _userManager.DeleteAsync(user);
        }
    }
}
