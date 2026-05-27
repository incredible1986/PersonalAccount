using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IRepo<TModel> where TModel : Model
{
    // CREATE
    Task AddAsync(TModel model);

    // READ
    Task<TModel?> GetByIdAsync(int id);
    Task<List<TModel>> GetAllAsync();
    
    // DELETE 
    Task DeleteByIdAsync(int id);
}