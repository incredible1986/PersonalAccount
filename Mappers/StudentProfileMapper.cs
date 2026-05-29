using PersonalAccount.Constants;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Utils;

namespace PersonalAccount.Mappers;

public class StudentProfileMapper : IMapper<StudentProfileEntity, StudentProfileModel>
{
    public StudentProfileEntity ToEntity(StudentProfileModel model) =>
        new()
        {
            Id = model.Id,
            AccountId = model.AccountId,
            GroupId = model.GroupId == GroupConstants.NoGroupId ? null : model.GroupId,
            FullName = model.FullName,
            PhotoUrl = model.PhotoUrl?.ToString()
        };

    public StudentProfileModel ToModel(StudentProfileEntity entity) =>
        new()
        {
            Id = entity.Id,
            AccountId = entity.AccountId,
            GroupId = entity.GroupId ?? GroupConstants.NoGroupId,
            FullName = entity.FullName,
            PhotoUrl = entity.PhotoUrl?.ToUri()
        };
}