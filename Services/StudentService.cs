using PersonalAccount.Models;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services;

public class StudentService(IAccountRepo<StudentAccountModel> accounts) : IStudentService
{
    public Task<StudentAccountModel?> GetByIdAsync(int id) => accounts.GetByIdAsync(id);
}