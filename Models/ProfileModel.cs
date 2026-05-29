namespace PersonalAccount.Models;

public abstract class ProfileModel : Model
{
    public int AccountId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public Uri? PhotoUrl { get; set; }
}