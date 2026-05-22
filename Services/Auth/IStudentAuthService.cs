using PersonalAccount.Models;

namespace PersonalAccount.Services.Auth
{
    public interface IStudentAuthService
    {
        Task<StudentProfileModel?> ValidateStudentAsync(string email, string password);
        Task SignInAsync(HttpContext ctx, StudentProfileModel studentProfile);
        Task SignOutAsync(HttpContext ctx);
    }
}