using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Types;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize(Roles = AccountRoleConstants.Administrator)]
public class AdminCabinetController(IAdminCabinetService cabinet) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accounts = await cabinet.GetAllStudentAccounts();
        var profiles = await cabinet.GetAllStudentProfiles();

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
}