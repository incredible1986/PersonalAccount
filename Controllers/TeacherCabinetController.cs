using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Constants;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize(Roles = AccountRoleConstants.Teacher)]
public class TeacherCabinetController(ITeacherCabinetService cabinetService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(new TeacherCabinetViewModel());
    }
}