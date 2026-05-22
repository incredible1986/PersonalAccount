using PersonalAccount.Models;

namespace PersonalAccount.Services.Auth
{
    public interface IStudentAuthService
    {
        Task<StudentAccountModel?> ValidateStudentAsync(string email, string password);
        Task SignInAsync(HttpContext ctx, StudentAccountModel studentAccount);
        Task SignOutAsync(HttpContext ctx);
    }
}