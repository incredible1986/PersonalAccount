using PersonalAccount.Models;

namespace PersonalAccount.Services.Auth
{
    public interface IStudentAuthService
    {
        Task<AccountModel?> ValidateStudentAsync(string email, string password);
        Task SignInAsync(HttpContext ctx, AccountModel account);
        Task SignOutAsync(HttpContext ctx);
    }
}