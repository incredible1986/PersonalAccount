using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models.Students;
using PersonalAccount.Utils;

namespace PersonalAccount.Services.Db;

public class DbSeeder(AppDbContext context, IPasswordHasher<StudentAuthModel> hasher)
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();
        var hasStudents = await context.Students.AnyAsync();
        if (hasStudents) return;

        var model = new StudentAuthModel
        {
            FullName = "John Doe",
            GroupName = "PD-412",
            Email = "example@top",
            PhotoUrl = "https://masterpiecer-images.s3.yandex.net/5fd531dca6427c7:upscaled".ToUri(),
        };

        var entity = new StudentEntity
        {
            FullName = model.FullName,
            GroupName = model.GroupName,
            Email = model.Email,
            PhotoUrl = model.PhotoUrl?.ToString(),
            PasswordHash = hasher.HashPassword(model, "example")
        };
        
        await context.Students.AddAsync(entity);
        await context.SaveChangesAsync();
    }
}
