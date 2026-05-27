using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IConfirmationTokenRepo
{
    Task AddAsync(ConfirmationTokenModel token);
    Task<List<ConfirmationTokenModel>> GetByAccountIdAsync(int accountId);
    Task ConfirmByIdAsync(int id);
}