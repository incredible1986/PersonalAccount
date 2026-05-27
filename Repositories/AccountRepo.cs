using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Mappers;
using PersonalAccount.Models;
using PersonalAccount.Types;

namespace PersonalAccount.Repositories;

public class AccountRepo(AppDbContext context, IMapper<AccountEntity, AccountModel> mapper)
    : Repo<AccountEntity, AccountModel>(context, mapper, () => context.Accounts), IAccountRepo
{
    private DbSet<AccountEntity> Accounts => Context.Accounts;

    public async Task<AccountModel?> GetByEmailAsync(string email)
    {
        var entity = await Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Email == email);
        return entity == null ? null : Mapper.ToModel(entity);
    }

    public async Task<List<AccountModel>> GetAllByRoleAsync(AccountRoles role)
    {
        return await Accounts
            .AsNoTracking()
            .Where(entity => entity.Role == role)
            .Select(entity => Mapper.ToModel(entity))
            .ToListAsync();
    }

    public async Task<bool> AnyAsync() => await Accounts.AnyAsync();
}