using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Services.Confirmation;
using PersonalAccount.Types;
using PersonalAccount.Utils;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController(
    IStudentCabinetService studentCabinet,
    IAdminCabinetService adminCabinet,
    IConfirmationTokenService confirmation)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var role = User.GetRole();
        if (role == null) return Forbid();

        return role.Value switch
        {
            AccountRoles.Administrator => RedirectToAction("Administrator"),
            AccountRoles.Student => RedirectToAction("Student"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpGet]
    [Authorize(Roles = AccountRoleConstants.Administrator)]
    public async Task<IActionResult> Administrator()
    {
        var accounts = await adminCabinet.GetAllStudentAccounts();
        var profiles = await adminCabinet.GetAllStudentProfiles();

        return View(new AdminCabinetViewModel
        {
            Students = profiles.Select(profile => new AdminCabinetStudentViewModel
            {
                Email = accounts[profile.AccountId].Email,
                FullName = profile.FullName,
                GroupName = profile.GroupName,
                PhotoUrl = profile.PhotoUrl?.ToString(),
            }).ToList()
        });
    }

    [HttpGet]
    [Authorize(Roles = AccountRoleConstants.Student)]
    public async Task<IActionResult> Student()
    {
        var accountId = User.GetId();
        var accountEmail = User.GetEmail();
        if (accountId == null || accountEmail == null) return RedirectToAction("Error", "Home");

        var student = await studentCabinet.GetByAccountIdAsync(accountId.Value);
        if (student == null) return RedirectToAction("Error", "Home");

        var isEmailConfirmed = await confirmation.HasAnyConfirmedTokenAsync(student.Id);

        return View(new StudentCabinetViewModel
        {
            Email = accountEmail,
            FullName = student.FullName,
            GroupName = student.GroupName,
            PhotoUrl = student.PhotoUrl?.ToString(),
            IsEmailConfirmed = isEmailConfirmed
        });
    }
}