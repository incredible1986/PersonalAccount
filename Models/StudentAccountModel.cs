namespace PersonalAccount.Models;

public class StudentAccountModel : AccountModel
{
    public int ProfileId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public Uri? PhotoUrl { get; set; }
}