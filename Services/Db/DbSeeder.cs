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
        var hasStudents = await context.StudentProfiles.AnyAsync();
        if (hasStudents) return;

        var account = new AccountModel
        {
            Email = "shamraev.alexandr@gmail.com"
        };

        var accountEntity = accountMapper.ToEntity(account);
        accountEntity.PasswordHash = hasher.HashPassword(account, "example");

        await context.Accounts.AddAsync(accountEntity);
        await context.SaveChangesAsync();

        accountEntity = await context.Accounts.AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Email == account.Email) ?? throw new InvalidOperationException();

        var studentProfile = new StudentProfileModel
        {
            AccountId = accountEntity.Id,
            FullName = "John Doe",
            GroupName = "PD-412",
            PhotoUrl = "https://masterpiecer-images.s3.yandex.net/5fd531dca6427c7:upscaled".ToUri(),
        };
        var studentProfileEntity = studentProfileMapper.ToEntity(studentProfile);
        await context.StudentProfiles.AddAsync(studentProfileEntity);
        await context.SaveChangesAsync();
    }
}