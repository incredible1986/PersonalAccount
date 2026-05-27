using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Mappers;

namespace PersonalAccount.Repositories;

public abstract class Repo<TEntity, TModel>(
    AppDbContext context,
    IMapper<TEntity, TModel> mapper,
    Func<DbSet<TEntity>> getTable)
    : IRepo<TModel>
    where TModel : class
    where TEntity : class
{
    protected readonly AppDbContext Context = context;
    protected readonly IMapper<TEntity, TModel> Mapper = mapper;

    private DbSet<TEntity> Table => getTable();

    public async Task AddAsync(TModel model)
    {
        await Table.AddAsync(Mapper.ToEntity(model));
        await Context.SaveChangesAsync();
    }

    public async Task<TModel?> GetByIdAsync(int id)
    {
        var entity = await Table.FindAsync(id);
        return entity == null ? null : Mapper.ToModel(entity);
    }

    public async Task<List<TModel>> GetAllAsync()
    {
        return await Table.AsNoTracking()
            .Select(entity => Mapper.ToModel(entity))
            .ToListAsync();
    }
}