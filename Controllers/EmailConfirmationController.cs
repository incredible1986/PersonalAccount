using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Services.Confirmation;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

public class EmailConfirmationController(IConfirmationTokenService confirmation) : Controller
{
    [HttpGet]
    public IActionResult Index(int studentId, string token) => View();
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> SendEmailConfirmation()
    {
        var studentId = User.GetId();
        if (studentId == null) return RedirectToAction("Error", "Home");
        var token = confirmation.GenerateTokenAsync(studentId.Value);
        var confirmationUrl = Url.Action("Index", "EmailConfirmation", new
        {
            studentId, token
        }, Request.Scheme);
        
        // TODO: отправка письма через сервис. В письмо крепится confirmationUrl
        return RedirectToAction("Index", "Cabinet");
    }
}