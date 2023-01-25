using Microsoft.AspNetCore.Identity;
using WebUI.Models;
using WebUI.Models.Enum;

namespace WebUI.DataAccess;
public interface IAccountData
{
    Task<IdentityResult> RegisterUser(RegisterUserModel registerUser);

    Task<SignInResult> SignInUser(SignInUserModel signInUser);

    Task LogOutUser();

    Task UpdateUsersStatus(IEnumerable<UserViewModel> users, Status status, bool isBlocked);

    Task DeleteUsers(IEnumerable<UserViewModel> users);
}