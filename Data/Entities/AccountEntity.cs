using PersonalAccount.Types;

namespace PersonalAccount.Data.Entities;

public class AccountEntity : Entity
{
    public List<ConfirmationTokenEntity> ConfirmationTokens { get; set; } = [];
    public List<TeacherGroupDisciplineEntity> TeacherGroupDisciplines { get; set; } = [];
    public StudentProfileEntity? StudentProfile { get; set; }
    public TeacherProfileEntity? TeacherProfile { get; set; }
    
    public AccountRoles Role { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}