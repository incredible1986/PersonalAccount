using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Utils;

namespace PersonalAccount.Services.Db;

public class DbSeeder(AppDbContext context, IPasswordHasher<StudentProfileAuthModel> hasher)
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();
        var hasStudents = await context.StudentProfiles.AnyAsync();
        if (hasStudents) return;

        var model = new StudentProfileAuthModel
        {
            FullName = "John Doe",
            GroupName = "PD-412",
            Email = "example@top",
            PhotoUrl = "https://masterpiecer-images.s3.yandex.net/5fd531dca6427c7:upscaled".ToUri(),
        };

        var entity = new StudentProfileEntity
        {
            FullName = model.FullName,
            GroupName = model.GroupName,
            Email = model.Email,
            PhotoUrl = model.PhotoUrl?.ToString(),
            PasswordHash = hasher.HashPassword(model, "example")
        };
        
        await context.StudentProfiles.AddAsync(entity);
        await context.SaveChangesAsync();
    }
}
