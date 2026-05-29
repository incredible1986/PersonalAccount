using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Mappers;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public class ConfirmationTokenRepo(
    AppDbContext context,
    IMapper<ConfirmationTokenEntity, ConfirmationTokenModel> mapper
) : Repo<ConfirmationTokenEntity, ConfirmationTokenModel>(context, mapper, () => context.ConfirmationTokens),
    IConfirmationTokenRepo
{
    private DbSet<ConfirmationTokenEntity> ConfirmationTokens => Context.ConfirmationTokens;

    public async Task<List<ConfirmationTokenModel>> GetByAccountIdAsync(int accountId) =>
        await ConfirmationTokens
            .AsNoTracking()
            .Where(entity => entity.AccountId == accountId)
            .Select(entity => Mapper.ToModel(entity))
            .ToListAsync();

    public async Task ConfirmByIdAsync(int id)
    {
        var entity = await ConfirmationTokens.FindAsync(id) ?? throw new KeyNotFoundException();
        entity.ConfirmedAt = DateTime.UtcNow;
        await Context.SaveChangesAsync();
    }
}