using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Mappers;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public class StudentProfileRepo(AppDbContext context, IMapper<StudentProfileEntity, StudentProfileModel> mapper)
    : ProfileRepo<StudentProfileEntity, StudentProfileModel>(context, mapper, ctx => ctx.StudentProfiles),
        IStudentProfileRepo;