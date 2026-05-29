namespace PersonalAccount.ViewModels;

public class AdminCabinetGroupViewModel : CabinetGroupViewModel
{
    public string Description { get; set; } = string.Empty;
}

public class AdminCabinetStudentViewModel : ViewModel
{
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
}

public class AdminCabinetViewModel : ViewModel
{
    public List<int> GroupIdsOrder { get; set; } = [];
    public Dictionary<int, AdminCabinetGroupViewModel> Groups { get; set; } = [];
    public Dictionary<int, List<AdminCabinetStudentViewModel>> StudentProfiles { get; set; } = [];
}