using PersonalAccount.Models;
using PersonalAccount.Repositories;
using PersonalAccount.Types;
using PersonalAccount.Utils;

namespace PersonalAccount.Services.Cabinet;

public class AdminCabinetService(
    IAccountRepo accountRepo,
    IGroupRepo groupRepo,
    IDisciplineRepo disciplineRepo,
    IStudentProfileRepo studentProfileRepo,
    ITeacherProfileRepo teacherProfileRepo,
    ITeacherGroupDisciplineRepo teacherGroupDisciplineRepo
) : IAdminCabinetService
{
    public async Task<List<AccountModel>> GetAllStudentAndTeacherAccountsAsync() =>
        await accountRepo.GetAllByRoleAsync(AccountRoles.Student | AccountRoles.Teacher);

    public async Task<List<GroupModel>> GetAllGroupsAsync() => await groupRepo.GetAllAsync();

    public async Task<List<StudentProfileModel>> GetAllStudentProfilesAsync() => await studentProfileRepo.GetAllAsync();
    public async Task<List<TeacherProfileModel>> GetAllTeacherProfilesAsync() => await teacherProfileRepo.GetAllAsync();
    public async Task<List<DisciplineModel>> GetAllDisciplinesAsync() => await disciplineRepo.GetAllAsync();

    public async Task AddStudentProfileAsync(string email, string fullName) =>
        await AddProfileAsync(studentProfileRepo, email, fullName);

    public async Task AddTeacherProfileAsync(string email, string fullName) =>
        await AddProfileAsync(teacherProfileRepo, email, fullName);

    public async Task AddTeacherGroupDisciplineAsync(int teacherAccountId, int groupId, int disciplineId) =>
        await teacherGroupDisciplineRepo.AddAsync(new TeacherGroupDisciplineModel
        {
            DisciplineId = disciplineId,
            GroupId = groupId,
            TeacherAccountId = teacherAccountId
        });

    private async Task AddProfileAsync<TProfileModel>(
        IProfileRepo<TProfileModel> profileRepo,
        string email,
        string fullName
    ) where TProfileModel : ProfileModel, new()
    {
        var account = await accountRepo.GetByEmailAsync(email);
        if (account == null) return;

        await profileRepo.AddAsync(new TProfileModel
        {
            FullName = fullName,
            AccountId = account.Id
        });
    }

    public async Task AddGroupAsync(string name, string description, string? imageUrl)
    {
        await groupRepo.AddAsync(new GroupModel
        {
            Name = name,
            Description = description,
            ImageUrl = imageUrl?.ToUri()
        });
    }

    public async Task AddDisciplineAsync(string name)
    {
        await disciplineRepo.AddAsync(new DisciplineModel
        {
            Name = name
        });
    }
}