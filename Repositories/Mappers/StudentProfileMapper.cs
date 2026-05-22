using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Utils;

namespace PersonalAccount.Repositories.Mappers;

public class StudentProfileMapper : IMapper<StudentProfileEntity, StudentAccountModel>
{
    public StudentProfileEntity ToEntity(StudentAccountModel model) =>
        new()
        {
            Id = model.ProfileId,
            GroupName = model.GroupName,
            FullName = model.FullName,
            PhotoUrl = model.PhotoUrl?.ToString()
        };

    public StudentAccountModel ToModel(StudentProfileEntity entity) =>
        new()
        {
            ProfileId = entity.Id,
            GroupName = entity.GroupName,
            FullName = entity.FullName,
            PhotoUrl = entity.PhotoUrl?.ToUri()
        };
}