using PersonalAccount.Models;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services.Cabinet;

public class AdminCabinetService : IAdminCabinetService
{
    private readonly IAccountRepo _accounts;
    private readonly IStudentProfileRepo _studentProfiles;
    private readonly IConfirmationTokenRepo _confirmationRepo;

    public AdminCabinetService(IAccountRepo accounts, IStudentProfileRepo studentProfiles, IConfirmationTokenRepo confirmationRepo)
    {
        _accounts = accounts;
        _studentProfiles = studentProfiles;
        _confirmationRepo = confirmationRepo;
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

    public async Task ConfirmStudentEmailAsync(int accountId)
    {
        var token = Guid.NewGuid().ToString();
        var confirmation = new ConfirmationTokenModel
        {
            AccountId = accountId,
            TokenHash = Convert.ToHexString(
                System.Security.Cryptography.SHA256.HashData(
                    System.Text.Encoding.UTF8.GetBytes(token))),
            ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            ConfirmedAt = DateTime.UtcNow
        };

        await _confirmationRepo.CreateAsync(confirmation);
    }
}