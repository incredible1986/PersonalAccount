using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Services.Auth;

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

            var student = await auth.ValidateStudentAsync(model.Email, model.Password);
            if (student is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, student.Id.ToString()),
                new(ClaimTypes.Name, student.FullName),
                new(ClaimTypes.Email, student.Email),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Redirect(model.ReturnUrl ?? "/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Logout()
        {
            throw new NotImplementedException();
        }

        public IActionResult AccessDenied()
        {
            throw new NotImplementedException();
        }
    }
}