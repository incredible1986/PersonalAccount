using PersonalAccount.Data.Entities;
using PersonalAccount.Models.Students;
using PersonalAccount.Utils;

namespace PersonalAccount.Repositories.Mappers;

public class StudentAuthMapper : IMapper<StudentEntity, StudentAuthModel>
{
    public StudentEntity? ToEntity(StudentAuthModel? model) =>
        model is null
            ? null
            : new StudentEntity
            {
                Id = model.Id,
                Email = model.Email,
                GroupName = model.GroupName,
                PasswordHash = model.PasswordHash,
                FullName = model.FullName,
                PhotoUrl = model.PhotoUrl?.ToString()
            };

    public StudentAuthModel? ToModel(StudentEntity? entity) =>
        entity is null
            ? null
            : new StudentAuthModel
            {
                Id = entity.Id,
                Email = entity.Email,
                GroupName = entity.GroupName,
                PasswordHash = entity.PasswordHash,
                FullName = entity.FullName,
                PhotoUrl = entity.PhotoUrl?.ToUri()
            };
}