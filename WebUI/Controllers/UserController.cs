using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data;
using WebUI.DataAccess;
using WebUI.Models;
using WebUI.Models.Enum;

namespace WebUI.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IAccountData _accountData;

    public UserController(ApplicationDbContext db, IAccountData accountData)
    {
        _db = db;
        _accountData = accountData;
    }

    public IActionResult Index()
    {
        IEnumerable<ApplicationUserModel> data = _db.Users;
        var users = ConvertUserModelListToView(data);
        return View(users);
    }

    private IEnumerable<UserViewModel> ConvertUserModelListToView(IEnumerable<ApplicationUserModel> users)
    {
        var output = new List<UserViewModel>();
        foreach (var user in users)
        {
            output.Add(user);
        }
        return output;
    }

    public async Task<IActionResult> Block(IEnumerable<UserViewModel> users)
    {
        await _accountData.UpdateUsersStatus(users, Status.Blocked, true);
        return RedirectToAction("Index");
    }

    public async Task <IActionResult> Unblock(IEnumerable<UserViewModel> users)
    {
        await _accountData.UpdateUsersStatus(users, Status.Active, false);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(IEnumerable<UserViewModel> users)
    {
        await _accountData.DeleteUsers(users);
        return RedirectToAction("Index");
    }
}
