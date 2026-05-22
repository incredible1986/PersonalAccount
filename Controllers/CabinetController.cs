using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Services;
using PersonalAccount.Services.Confirmation;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController(IStudentService cabinet, IConfirmationTokenService confirmation) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var studentId = User.GetId();
        if (studentId == null) return RedirectToAction("Error", "Home");
        var student = await cabinet.GetByIdAsync(studentId.Value);
        if (student == null ) return RedirectToAction("Error", "Home");
        
        var isEmailConfirmed = await confirmation.HasAnyConfirmedTokenAsync(student.Id);
        
        return View(new StudentCabinetViewModel
        {
            Email =  student.Email,
            FullName = student.FullName,
            GroupName =  student.GroupName,
            PhotoUrl = student.PhotoUrl?.ToString(),
            IsEmailConfirmed = isEmailConfirmed
        });
    }
}