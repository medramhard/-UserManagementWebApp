using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers;
public class UserController : Controller
{
    // TODO: Put all the operations on user table here
    public IActionResult Index()
    {
        return View();
    }
}
