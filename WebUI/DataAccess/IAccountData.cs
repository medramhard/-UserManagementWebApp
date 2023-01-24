using Microsoft.AspNetCore.Identity;
using WebUI.Models;

namespace WebUI.DataAccess;
public interface IAccountData
{
    Task<IdentityResult> RegisterUser(RegisterUserModel registerUser);

    Task<SignInResult> SignInUser(SignInUserModel signInUser);
    Task LogOutUser();
}