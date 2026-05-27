namespace PersonalAccount.Repositories;

public interface IRepo<TModel> where TModel : class
{
    // CREATE
    Task AddAsync(TModel model);

    // READ
    Task<TModel?> GetByIdAsync(int id);
    Task<List<TModel>> GetAllAsync();
}