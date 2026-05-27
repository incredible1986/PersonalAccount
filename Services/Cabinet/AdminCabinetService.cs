using PersonalAccount.Models;
using PersonalAccount.Repositories;
using PersonalAccount.Types;

namespace PersonalAccount.Services.Cabinet;

public class AdminCabinetService(
    IAccountRepo accountRepo,
    IStudentProfileRepo studentProfileRepo
) : IAdminCabinetService
{
    public async Task<Dictionary<int, AccountModel>> GetAllStudentAccountsAsync()
    {
        var studentAccounts = await accountRepo.GetAllByRoleAsync(AccountRoles.Student);
        return studentAccounts.ToDictionary(account => account.Id);
    }

    public async Task<List<StudentProfileModel>> GetAllStudentProfilesAsync() => await studentProfileRepo.GetAllAsync();

    public async Task AddStudentProfileAsync(string email, string fullName, string groupName)
    {
        var account = await accountRepo.GetByEmailAsync(email);
        if (account == null) return;

        await studentProfileRepo.AddAsync(new StudentProfileModel
        {
            FullName = fullName,
            GroupName = groupName,
            AccountId = account.Id
        });
    }
}