using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Services;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController(IStudentService cabinet) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var studentId = User.GetId();
        if (studentId == null) return RedirectToAction("Error", "Home");
        var student = await cabinet.GetByIdAsync(studentId.Value);
        if (student == null ) return RedirectToAction("Error", "Home");
        
        return View(student);
    }
}