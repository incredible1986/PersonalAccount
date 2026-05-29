namespace PersonalAccount.Models;

public class TeacherGroupDisciplineModel : Model
{
    public int DisciplineId { get; set; }
    public int GroupId { get; set; }
    public int TeacherAccountId { get; set; }
}