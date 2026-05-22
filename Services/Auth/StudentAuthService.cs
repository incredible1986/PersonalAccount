using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services.Auth
{
    public class StudentAuthService(IAccountRepo<StudentProfileAuthModel> accounts, IPasswordHasher<StudentProfileAuthModel> hasher)
        : IStudentAuthService
    {
        public async Task<StudentProfileModel?> ValidateStudentAsync(string email, string password)
        {
            var student = await accounts.GetByEmailAsync(email);
            if (student is null) return null;

            var result = hasher.VerifyHashedPassword(student, student.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed) return null;
            return student.Clone() as StudentProfileModel;
        }

        public async Task SignInAsync(HttpContext ctx, StudentProfileModel studentProfile)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, studentProfile.Id.ToString()),
                new(ClaimTypes.Name, studentProfile.FullName),
                new(ClaimTypes.Email, studentProfile.Email),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        public async Task SignOutAsync(HttpContext ctx) => await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}