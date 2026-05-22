using PersonalAccount.Data.Entities;
using PersonalAccount.Utils;

namespace PersonalAccount.Repositories.Mappers;

public class StudentAuthMapper : IMapper<StudentProfileEntity, StudentProfileAuthModel>
{
    public StudentProfileEntity? ToEntity(StudentProfileAuthModel? model) =>
        model is null
            ? null
            : new StudentProfileEntity
            {
                Id = model.Id,
                Email = model.Email,
                GroupName = model.GroupName,
                PasswordHash = model.PasswordHash,
                FullName = model.FullName,
                PhotoUrl = model.PhotoUrl?.ToString()
            };

    public StudentProfileAuthModel? ToModel(StudentProfileEntity? entity) =>
        entity is null
            ? null
            : new StudentProfileAuthModel
            {
                Id = entity.Id,
                Email = entity.Email,
                GroupName = entity.GroupName,
                PasswordHash = entity.PasswordHash,
                FullName = entity.FullName,
                PhotoUrl = entity.PhotoUrl?.ToUri()
            };
}