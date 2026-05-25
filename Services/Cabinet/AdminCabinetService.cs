using PersonalAccount.Models;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services.Cabinet;

public class AdminCabinetService : IAdminCabinetService
{
    private readonly IAccountRepo _accounts;
    private readonly IStudentProfileRepo _studentProfiles;

    public AdminCabinetService(IAccountRepo accounts, IStudentProfileRepo studentProfiles)
    {
        _accounts = accounts;
        _studentProfiles = studentProfiles;
    }

    public async Task<Dictionary<int, AccountModel>> GetAllStudentAccountsAsync()
    {
        var accounts = await _accounts.GetByRoleAsync(AccountRole.Student);
        return accounts.ToDictionary(account => account.Id);
    }

    public async Task<List<StudentProfileModel>> GetAllStudentProfilesAsync()
    {
        return await _studentProfiles.GetAllAsync();
    }
}