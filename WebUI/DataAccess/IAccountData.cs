using Microsoft.AspNetCore.Identity;
using WebUI.Models;
using WebUI.Models.Enum;

namespace WebUI.DataAccess;
public interface IAccountData
{
    Task<IdentityResult> RegisterUser(RegisterUserModel registerUser);

    Task<SignInResult> SignInUser(SignInUserModel signInUser);

    Task LogOutUser();

    Task BlockUsers(IEnumerable<string> usernames);

    Task UnblockUsers(IEnumerable<string> usernames);

    Task DeleteUsers(IEnumerable<string> usernames);
}