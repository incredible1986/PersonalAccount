using PersonalAccount.Models;

namespace PersonalAccount.Services;

public interface IStudentService
{
    Task<StudentProfileModel?> GetByIdAsync(int id);
}