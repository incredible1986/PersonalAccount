using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IProfileRepo<TEntity, TModel> where TModel : AccountModel
{
}