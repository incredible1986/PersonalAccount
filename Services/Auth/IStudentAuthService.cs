using PersonalAccount.Models;

namespace PersonalAccount.Services.Auth
{
    public interface IStudentAuthService
    {
        Task<AccountModel?> ValidateAsync(string email, string password);
        Task SignInAsync(HttpContext ctx, AccountModel account);
        Task SignOutAsync(HttpContext ctx);
    }
}