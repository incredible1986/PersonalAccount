using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models.Students;
using PersonalAccount.Repositories.Mappers;

namespace PersonalAccount.Repositories;

public class StudentRepo<T>(AppDbContext context, IMapper<StudentEntity, T> mapper) : IStudentRepo<T> where T : StudentModel
{
    private DbSet<StudentEntity> Students => context.Students;


    public async Task<T?> GetByIdAsync(int id)
    {
        var entity = await Students
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        return mapper.ToModel(entity);
    }

    public async Task UpdateByIdAsync(int id, T student)
    {
        var entity = await Students.FindAsync(id)
            ?? throw new InvalidOperationException($"Student with id {id} not found");

        entity.FullName = student.FullName;
        entity.GroupName = student.GroupName;
        entity.PhotoUrl = student.PhotoUrl?.ToString();

        await context.SaveChangesAsync();
    }
    public async Task<T?> GetByEmailAsync(string email)
    {
        var entity = await Students
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Email == email);
        return mapper.ToModel(entity);
    }

    public async Task UpdatePasswordHashAsync(int id, string passwordHash)
    {
        var entity = await Students.FindAsync(id)
            ?? throw new InvalidOperationException($"Student with id {id} not found");

        entity.PasswordHash = passwordHash;
        await context.SaveChangesAsync();
    }
}