using PersonalAccount.Models;
using PersonalAccount.Models.Students;

namespace PersonalAccount.Services
{
    public interface IStudentService
    {
        Task<StudentModel?> GetByIdAsync(int id);
        Task UpdateByIdAsync(int id, StudentEditViewModel model);
    }
}