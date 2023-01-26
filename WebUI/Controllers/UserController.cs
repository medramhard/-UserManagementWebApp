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
        UserViewModel model = new()
        {
            Users = ConvertUserModelListToView(data)
        };
        return View(model);
    }

    private IEnumerable<UserModel> ConvertUserModelListToView(IEnumerable<ApplicationUserModel> users)
    {
        var output = new List<UserModel>();
        foreach (var user in users)
        {
            output.Add(user);
        }
        return output;
    }

    [HttpPost]
    public async Task<IActionResult> Block(UserViewModel model)
    {
        await _accountData.BlockUsers(model.CheckedUsers);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task <IActionResult> Unblock(UserViewModel model)
    {
        await _accountData.UnblockUsers(model.CheckedUsers);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(UserViewModel model)
    {
        await _accountData.DeleteUsers(model.CheckedUsers);
        return RedirectToAction("Index");
    }
}
