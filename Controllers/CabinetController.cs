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

        return role.Value switch
        {
            AccountRoles.Administrator => RedirectToAction("Index", "AdminCabinet"),
            AccountRoles.Student => RedirectToAction("Index", "StudentCabinet"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}