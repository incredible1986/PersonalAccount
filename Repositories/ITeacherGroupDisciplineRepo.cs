using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface ITeacherGroupDisciplineRepo : IRepo<TeacherGroupDisciplineModel>
{
    Task<List<TeacherGroupDisciplineModel>> GetAllByTeacherAccountIdAsync(int teacherAccountId);

    Task<List<TeacherGroupDisciplineModel>> GetAllByTeacherAccountIdAndDisciplineIdAsync(
        int teacherAccountId,
        int disciplineId
    );
}