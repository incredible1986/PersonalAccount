using PersonalAccount.Models;
using PersonalAccount.Types;

namespace PersonalAccount.Services.Cabinet;

public interface IAdminCabinetService
{
    Task<Dictionary<int, AccountModel>> GetAllStudentAccountsAsync();
    Task<List<StudentProfileModel>> GetAllStudentProfilesAsync();
    Task AddStudentProfileAsync(string email, string fullName, string groupName);
}