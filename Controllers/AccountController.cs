using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Services.Auth;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers
{
    public class AccountController(IStudentAuthService auth) : Controller
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var account = await auth.ValidateAsync(model.Email, model.Password);
            if (account is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View(model);
            }
            
            await auth.SignInAsync(HttpContext, account);
            return Redirect(model.ReturnUrl ?? "/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await auth.SignOutAsync(HttpContext);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            throw new NotImplementedException();
        }
    }
}