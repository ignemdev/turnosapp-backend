using AutoMapper;
using Queues.Application.Generic.DTOs;
using Queues.Application.Generic.Interfaces;
using Queues.Domain.Entities;

namespace Queues.Application.Generic.Handlers;
public class BaseCrudHandler<TDto, TEntity> : IBaseCrudHandler<TDto, TEntity> where TDto : BaseDto where TEntity : BaseEntity
{
    protected readonly IBaseCrudService<TEntity> _crudService;
    protected readonly IMapper _mapper;

    public BaseCrudHandler(IBaseCrudService<TEntity> crudService, IMapper mapper)
    {
        _crudService = crudService;
        _mapper = mapper;
    }

    public virtual Task<IQueryable<TEntity>> Query()
    {
        return Task.FromResult(_crudService.Query());
    }

    public virtual async Task<TDto> GetById(int id)
    {
        var entity = await _crudService.GetById(id);
        var dto = _mapper.Map<TDto>(entity);
        return dto;
    }

    public virtual async Task<TDto> Create(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity = await _crudService.Create(entity);
        return _mapper.Map(entity, dto);
    }

    public virtual async Task<TDto> Update(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity = await _crudService.Update(entity);
        return _mapper.Map(entity, dto);
    }

    public virtual async Task<TDto> Update(int id, TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity = await _crudService.Update(id, entity);
        return _mapper.Map(entity, dto);
    }

    public virtual async Task<bool> Delete(int id)
    {
        return await _crudService.Delete(id);
    }
}