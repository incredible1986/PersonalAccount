using PersonalAccount.Models.Students;

namespace PersonalAccount.Services;

public interface IStudentService
{
    Task<StudentModel?> GetByIdAsync(int id);
}