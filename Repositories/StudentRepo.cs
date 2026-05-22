using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Repositories.Mappers;

namespace PersonalAccount.Repositories;

public class StudentRepo<T>(AppDbContext context, IMapper<StudentProfileEntity, T> mapper) : IStudentRepo<T> where T : StudentProfileModel
{
    private DbSet<StudentProfileEntity> Students => context.StudentProfiles;
    
    public async Task<T?> GetByEmailAsync(string email)
    {
        var entity = await Students
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Email == email);
        return mapper.ToModel(entity);
    }
    
    public async Task<T?> GetByIdAsync(int id)
    {
        var entity = await Students.FindAsync(id);
        return mapper.ToModel(entity);
    }
}