namespace PersonalAccount.Models;

public class AccountModel
{
    public int AccountId { get; set; }

    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}