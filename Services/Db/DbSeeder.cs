using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Repositories.Mappers;
using PersonalAccount.Utils;

namespace PersonalAccount.Services.Db;

public class DbSeeder(
    AppDbContext context,
    IPasswordHasher<AccountModel> hasher,
    IMapper<AccountEntity, AccountModel> accountMapper,
    IMapper<StudentProfileEntity, StudentProfileModel> studentProfileMapper)
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();
        var hasAccounts = await context.Accounts.AnyAsync();
        if (hasAccounts) return;

        var account = new AccountModel
        {
            Email = "incrediblemej@gmail.com",
            Role = AccountRole.Admin
        };

        var accountEntity = accountMapper.ToEntity(account);
        accountEntity.PasswordHash = hasher.HashPassword(account, "example");

        await context.Accounts.AddAsync(accountEntity);
        await context.SaveChangesAsync();

        accountEntity = await context.Accounts.AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Email == account.Email) ?? throw new InvalidOperationException();
    }
}