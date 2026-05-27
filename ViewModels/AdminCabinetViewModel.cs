namespace PersonalAccount.ViewModels;

public class AdminCabinetGroupViewModel : ViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}

public class AdminCabinetStudentViewModel : ViewModel
{
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
}

public class AdminCabinetViewModel : ViewModel
{
    public List<int> GroupsOrder { get; set; } = [];
    public Dictionary<int, AdminCabinetGroupViewModel> Groups { get; set; } = [];
    public Dictionary<int, List<AdminCabinetStudentViewModel>> StudentProfiles { get; set; } = [];
}