using PersonalAccount.Models;
using PersonalAccount.Repositories;
using PersonalAccount.Types;

namespace PersonalAccount.Services.Cabinet;

public class AdminCabinetService(IAccountRepo accounts, IStudentProfileRepo studentProfiles) : IAdminCabinetService
{
    public async Task<Dictionary<int, AccountModel>> GetAllStudentAccounts()
    {
        var studentAccounts = await accounts.GetAllByRoleAsync(AccountRoles.Student);
        return studentAccounts.ToDictionary(account => account.Id);
    }

    public async Task<List<StudentProfileModel>> GetAllStudentProfiles() => await studentProfiles.GetAllAsync();
}