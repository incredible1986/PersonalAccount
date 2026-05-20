using PersonalAccount.Models.Students;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services;

public class StudentService(IStudentRepo<StudentModel> students) : IStudentService
{
    public Task<StudentModel?> GetByIdAsync(int id) => students.GetByIdAsync(id);
}