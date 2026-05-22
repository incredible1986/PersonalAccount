using PersonalAccount.Models;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services;

public class StudentService(IStudentRepo<StudentProfileModel> students) : IStudentService
{
    public Task<StudentProfileModel?> GetByIdAsync(int id) => students.GetByIdAsync(id);
}