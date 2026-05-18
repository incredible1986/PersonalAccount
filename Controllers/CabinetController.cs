using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models.Students;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var studentId = User.GetId();
        if (studentId == null) return RedirectToAction("Error", "Home");
        
        return View(new StudentModel
        {
            Id = studentId.Value,
        });
    }
}