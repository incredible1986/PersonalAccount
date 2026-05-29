using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Types;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var role = User.GetRole();
        if (role == null) return Forbid();

        return RedirectToAction("Index", $"{role.Value.ToString()}Cabinet");
    }
}