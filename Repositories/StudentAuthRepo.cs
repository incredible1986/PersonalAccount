using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models.Students;
using PersonalAccount.Repositories.Mappers;

namespace PersonalAccount.Repositories;

public class StudentAuthRepo(AppDbContext context, IMapper<StudentEntity, StudentAuthModel> mapper) : IStudentRepo<StudentAuthModel>
{
    private DbSet<StudentEntity> Students => context.Students;
    
    public async Task<StudentAuthModel?> GetByEmailAsync(string email)
    {
        var entity = await Students
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Email == email);
        return mapper.ToModel(entity);
    }
}