using PersonalAccount.Models;
using PersonalAccount.Repositories;
using PersonalAccount.Types;

namespace PersonalAccount.Services.Cabinet;

public class AdminCabinetService(
    IAccountRepo accountRepo,
    IGroupRepo groupRepo,
    IStudentProfileRepo studentProfileRepo
) : IAdminCabinetService
{
    public async Task<List<AccountModel>> GetAllStudentAccountsAsync() =>
        await accountRepo.GetAllByRoleAsync(AccountRoles.Student);

    public async Task<List<GroupModel>> GetAllGroupsAsync() => await groupRepo.GetAllAsync();

    public async Task<List<StudentProfileModel>> GetAllStudentProfilesAsync() => await studentProfileRepo.GetAllAsync();

    public async Task AddStudentProfileAsync(string email, string fullName)
    {
        var account = await accountRepo.GetByEmailAsync(email);
        if (account == null) return;

        await studentProfileRepo.AddAsync(new StudentProfileModel
        {
            FullName = fullName,
            AccountId = account.Id
        });
    }
}