namespace PersonalAccount.ViewModels;

public class AdminCabinetStudentViewModel : ViewModel
{
    public int AccountId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public int GroupId { get; set; }
    public string? PhotoUrl { get; set; }
}

public class AdminCabinetTeacherViewModel : ViewModel
{
    public int AccountId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
}

public class AdminCabinetViewModel : ViewModel
{
    public List<AdminCabinetTeacherViewModel> Teachers { get; set; } = [];
    public List<AdminCabinetStudentViewModel> Students { get; set; } = [];
    public List<AdminCabinetGroupViewModel> Groups { get; set; } = [];
    public List<AdminCabinetDisciplineViewModel> Disciplines { get; set; } = [];
}

public class AdminCabinetGroupViewModel : ViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}

public class AdminCabinetDisciplineViewModel : ViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}