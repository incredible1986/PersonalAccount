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
public class CabinetController(IStudentCabinetService cabinet, IConfirmationTokenService confirmation) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accountId = User.GetId();
        var accountEmail = User.GetEmail();
        if (accountId == null || accountEmail == null) return RedirectToAction("Error", "Home");
        var student = await cabinet.GetByAccountIdAsync(accountId.Value);
        if (student == null ) return RedirectToAction("Error", "Home");
        
        var isEmailConfirmed = await confirmation.HasAnyConfirmedTokenAsync(student.Id);
        
        return View(new StudentCabinetViewModel
        {
            Email =  accountEmail,
            FullName = student.FullName,
            GroupName =  student.GroupName,
            PhotoUrl = student.PhotoUrl?.ToString(),
            IsEmailConfirmed = isEmailConfirmed
        });
    }
}