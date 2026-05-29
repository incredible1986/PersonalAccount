using PersonalAccount.Models;

public interface IAdminCabinetService
{
    Task<Dictionary<int, AccountModel>> GetAllStudentAccountsAsync();
    Task<List<StudentProfileModel>> GetAllStudentProfilesAsync();
    Task ConfirmStudentEmailAsync(int accountId);
}