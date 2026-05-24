using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Services;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Services.Confirmation;
using PersonalAccount.Utils;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        if (User.IsInRole(AccountRole.Admin.ToString()))
            return RedirectToAction("Admin");

        if (User.IsInRole(AccountRole.Student.ToString()))
            return RedirectToAction("Student");

        return Forbid();
    }

    [HttpGet]
    [Authorize(Roles = "Student")]
    public IActionResult Student()
    {
        // TODO: вернуть кабинет студента
        return View(/*...*/);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Admin()
    {
        // TODO: вернуть кабинет администратора
        return View(/*...*/);
    }
}