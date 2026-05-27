using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IConfirmationTokenRepo : IRepo<ConfirmationTokenModel>
{
    Task<List<ConfirmationTokenModel>> GetByAccountIdAsync(int accountId);
    Task ConfirmByIdAsync(int id);
}