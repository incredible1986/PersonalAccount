using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IStudentProfileRepo : IRepo<StudentProfileModel>
{
    Task<StudentProfileModel?> GetByAccountIdAsync(int accountId);
}