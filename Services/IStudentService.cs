using PersonalAccount.Models;

namespace PersonalAccount.Services;

public interface IStudentService
{
    Task<StudentAccountModel?> GetByIdAsync(int id);
}