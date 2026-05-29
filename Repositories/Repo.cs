using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Mappers;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public abstract class Repo<TEntity, TModel>(
    AppDbContext context,
    IMapper<TEntity, TModel> mapper,
    Func<DbSet<TEntity>> tableSelector)
    : IRepo<TModel>
    where TModel : Model
    where TEntity : Entity
{
    protected readonly AppDbContext Context = context;
    protected readonly IMapper<TEntity, TModel> Mapper = mapper;

    private DbSet<TEntity> Table => tableSelector();

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

    public async Task DeleteByIdAsync(int id)
    {
        var entity = await Table.FindAsync(id) ?? throw new KeyNotFoundException();
        Table.Remove(entity);
        await Context.SaveChangesAsync();
    }
}