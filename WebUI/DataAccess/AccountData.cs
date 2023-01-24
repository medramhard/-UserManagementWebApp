using Microsoft.AspNetCore.Identity;
using WebUI.Models;

namespace WebUI.DataAccess;

public class AccountData : IAccountData
{
    private readonly UserManager<ApplicationUserModel> _userManager;
    private readonly SignInManager<ApplicationUserModel> _signInManager;

    public AccountData(UserManager<ApplicationUserModel> userManager, SignInManager<ApplicationUserModel> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
        var user = new ApplicationUserModel()
        {
            UserName = signInUser.Email,
            LastSeenDate = DateTime.UtcNow
        };
        await _userManager.UpdateAsync(user);
    }

    public async Task LogOutUser()
    {
        await _signInManager.SignOutAsync();
    }
}
