using PersonalAccount.Models;

namespace PersonalAccount.ViewModels;

public class StudentCabinetViewModel : CabinetViewModel
{
    public string FullName { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
}