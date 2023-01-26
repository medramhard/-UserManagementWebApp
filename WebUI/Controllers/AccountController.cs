using Microsoft.AspNetCore.Mvc;
using WebUI.DataAccess;
using WebUI.Models;

namespace WebUI.Controllers;
public class AccountController : Controller
{
    private readonly IAccountData _accountData;

    public AccountController(IAccountData accountData)
    {
        _accountData = accountData;
    }

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(RegisterUserModel user)
    {
        if (ModelState.IsValid)
        {
            var result = await _accountData.RegisterUser(user);
            if (result.Succeeded == false)
            {
                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError("", e.Description);
                }
                return View();
            }
        }
        return RedirectToAction("Index", "User");
    }

    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInUserModel user)
    {
        if (ModelState.IsValid)
        {
            var result = await _accountData.SignInUser(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", "Invalid credentials");
        }
        return View(user);
    }

    public async Task<IActionResult> LogOut()
    {
        await _accountData.LogOutUser();
        return RedirectToAction("SignIn");
    }
}
