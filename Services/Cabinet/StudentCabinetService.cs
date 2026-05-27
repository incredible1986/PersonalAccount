using PersonalAccount.Models;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services.Cabinet;

public class StudentCabinetService(IStudentProfileRepo studentProfileRepo) : IStudentCabinetService
{
    public Task<StudentProfileModel?> GetByAccountIdAsync(int accountId) =>
        studentProfileRepo.GetByAccountIdAsync(accountId);
}