using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Mappers;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public class StudentProfileRepo(AppDbContext context, IMapper<StudentProfileEntity, StudentProfileModel> mapper)
    : Repo<StudentProfileEntity, StudentProfileModel>(context, mapper, () => context.StudentProfiles),
        IStudentProfileRepo
{
    private DbSet<StudentProfileEntity> StudentProfiles => Context.StudentProfiles;

    public async Task<StudentProfileModel?> GetByAccountIdAsync(int accountId)
    {
        var entity = await StudentProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.AccountId == accountId);

        return entity == null ? null : Mapper.ToModel(entity);
    }
}