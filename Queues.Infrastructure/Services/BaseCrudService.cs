using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Queues.Application.Generic.Interfaces;
using Queues.Domain.Entities;
using Queues.Infrastructure.Context;

namespace Queues.Infrastructure.Services;
public class BaseCrudService<TEntity> : IBaseCrudService<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _dbSet;
    private readonly IQueuesDbContext _dbContext;
    private readonly IMapper _mapper;

    public BaseCrudService(IQueuesDbContext dbContext, IMapper mapper)
    {
        _dbSet = dbContext.GetDbSet<TEntity>();
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IQueryable<TEntity> Query()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<List<TEntity>> Get(int top = 50)
    {
        return await Query().Take(top).ToListAsync();
    }

    public async Task<TEntity> GetById(int id)
    {
        var queryResult = Query();

        var entity = await queryResult.FirstOrDefaultAsync(entity => entity.Id == id);

        return entity;
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        await _dbSet.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public async Task<TEntity> Update(TEntity entity)
    {
        _dbSet.Update(entity);

        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public async Task<TEntity> Update(int id, TEntity entity)
    {
        if (id != entity.Id)
            throw new InvalidOperationException("Id doesn't match");

        var existingEntity = await GetById(id);

        _mapper.Map(entity, existingEntity);

        _dbSet.Update(existingEntity);

        return existingEntity;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await GetById(id);

        if (entity == default)
            throw new InvalidOperationException("Error deleting entity.");

        _dbSet.Remove(entity);

        await _dbContext.SaveChangesAsync();

        return true;
    }
}