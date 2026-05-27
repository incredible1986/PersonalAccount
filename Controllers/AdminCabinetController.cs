using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Services.Account;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Services.Email;
using PersonalAccount.Types;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize(Roles = AccountRoleConstants.Administrator)]
public class AdminCabinetController(
    IAdminCabinetService cabinetService,
    IAccountService accountService,
    IEmailSenderService emailSenderService
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accounts = await cabinetService.GetAllStudentAccountsAsync();
        var profiles = await cabinetService.GetAllStudentProfilesAsync();

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
    public IActionResult AddStudent()
    {
        return View(new AddStudentViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddStudent(AddStudentViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var password = await accountService.RegisterAsync(model.Email, AccountRoles.Student);
        await cabinetService.AddStudentProfileAsync(model.Email, model.FullName, model.GroupName);
        await emailSenderService.SendEmailAsync(model.ContactEmail, "Данные для входа в личный кабинет",
            $"""
             <head></head>
             <body>
             <p>{model.Email}</p>
             <p>{password}</p>
             </body>
             """);

        return RedirectToAction("Index");
    }
}