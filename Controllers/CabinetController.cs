using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Models.Students;
using PersonalAccount.Services;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController(IStudentService studentService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var studentId = User.GetId();
        if (studentId == null) return RedirectToAction("Error", "Home");

        var student = await studentService.GetByIdAsync(studentId.Value);
        if (student == null) return RedirectToAction("Error", "Home");

        return View(student);
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var studentId = User.GetId();
        if (studentId == null) return RedirectToAction("Error", "Home");

        var student = await studentService.GetByIdAsync(studentId.Value);
        if (student == null) return RedirectToAction("Error", "Home");

        return View(new StudentEditViewModel
        {
            FullName = student.FullName,
            GroupName = student.GroupName,
            PhotoUrl = student.PhotoUrl?.ToString()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(StudentEditViewModel model)
    {
        var studentId = User.GetId();
        if (studentId == null) return RedirectToAction("Error", "Home");

        if (!ModelState.IsValid) return View(model);

        await studentService.UpdateByIdAsync(studentId.Value, model);
        return RedirectToAction("Index");
    }
}