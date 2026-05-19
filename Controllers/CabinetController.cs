using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Models.Students;
using PersonalAccount.Services;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController(
    IStudentService studentService,
    IPasswordService passwordService) : Controller
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

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View(new PasswordChangeViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(PasswordChangeViewModel model)
    {
        var studentId = User.GetId();
        if (studentId == null) return RedirectToAction("Error", "Home");

        if (!ModelState.IsValid) return View(model);

        if (model.OldPassword == model.NewPassword)
        {
            ModelState.AddModelError(string.Empty, "Новый пароль не должен совпадать со старым");
            return View(model);
        }

        var isValid = await passwordService.ValidatePasswordAsync(studentId.Value, model.OldPassword);
        if (!isValid)
        {
            ModelState.AddModelError(string.Empty, "Неверный текущий пароль");
            return View(model);
        }

        await passwordService.UpdatePasswordAsync(studentId.Value, model.NewPassword);
        return RedirectToAction("Index");
    }
}