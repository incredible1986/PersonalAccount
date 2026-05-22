using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Utils;

namespace PersonalAccount.Repositories.Mappers;

public class StudentMapper : IMapper<StudentProfileEntity, StudentProfileModel>
{
    public StudentProfileEntity? ToEntity(StudentProfileModel? model) =>
        model is null
            ? null
            : new StudentProfileEntity
            {
                Id = model.Id,
                Email = model.Email,
                GroupName = model.GroupName,
                FullName = model.FullName,
                PhotoUrl = model.PhotoUrl?.ToString()
            };

    public StudentProfileModel? ToModel(StudentProfileEntity? entity) =>
        entity is null
            ? null
            : new StudentProfileModel
            {
                Id = entity.Id,
                Email = entity.Email,
                GroupName = entity.GroupName,
                FullName = entity.FullName,
                PhotoUrl = entity.PhotoUrl?.ToUri()
            };
}