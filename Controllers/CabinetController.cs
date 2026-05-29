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
    private readonly IStudentCabinetService _cabinet;
    private readonly IConfirmationTokenService _confirmation;
    private readonly IAdminCabinetService _adminCabinet;

    public CabinetController(
     IStudentCabinetService cabinet,
     IConfirmationTokenService confirmation,
     IAdminCabinetService adminCabinet)
    {
        _cabinet = cabinet;
        _confirmation = confirmation;
        _adminCabinet = adminCabinet;
    }

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
    public async Task<IActionResult> Student()
    {
        var accountId = User.GetId();
        var accountEmail = User.GetEmail();
        if (accountId == null || accountEmail == null) return RedirectToAction("Error", "Home");
        var student = await _cabinet.GetByAccountIdAsync(accountId.Value);
        if (student == null) return RedirectToAction("Error", "Home");

        var isEmailConfirmed = await _confirmation.HasAnyConfirmedTokenAsync(accountId.Value);

        return View(new StudentCabinetViewModel
        {
            Email = accountEmail,
            FullName = student.FullName,
            GroupName = student.GroupName,
            PhotoUrl = student.PhotoUrl?.ToString(),
            IsEmailConfirmed = isEmailConfirmed
        });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Admin()
    {
        var accounts = await _adminCabinet.GetAllStudentAccountsAsync();
        var profiles = await _adminCabinet.GetAllStudentProfilesAsync();

        var students = new List<AdminCabinetStudentViewModel>();

        foreach (var profile in profiles)
        {
            if (accounts.TryGetValue(profile.AccountId, out var account))
            {
                var isConfirmed = await _confirmation.HasAnyConfirmedTokenAsync(profile.AccountId);
                students.Add(new AdminCabinetStudentViewModel
                {
                    FullName = profile.FullName,
                    GroupName = profile.GroupName,
                    PhotoUrl = profile.PhotoUrl?.ToString(),
                    IsEmailConfirmed = isConfirmed
                });
            }
        }

        return View(new AdminCabinetViewModel
        {
            Students = students
        });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmStudentEmail(int id)
    {
        await _adminCabinet.ConfirmStudentEmailAsync(id);
        return RedirectToAction("Admin");
    }
}