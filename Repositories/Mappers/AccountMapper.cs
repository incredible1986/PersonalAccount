using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories.Mappers;

public class AccountMapper : IMapper<AccountEntity, ProfileModel>
{
    public AccountEntity ToEntity(ProfileModel model) => new()
    {
        Id = model.AccountId,
        Email = model.Email,
        PasswordHash = model.PasswordHash
    };

    public ProfileModel ToModel(AccountEntity entity) => new()
    {
        AccountId = entity.Id,
        Email = entity.Email,
        PasswordHash = entity.PasswordHash
    };
}