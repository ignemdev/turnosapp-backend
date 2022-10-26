using AutoMapper;
using Queues.Application.Generic.Interfaces;
using Queues.Domain.Entities;

namespace Queues.Application.Generic.Handlers;
public class BaseCrudHandler<TEntity> : IBaseCrudHandler<TEntity> where TEntity : BaseEntity
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

    public async Task<List<TDto>> GetAll<TDto>(int top = 50)
    {
        var entities = await _crudService.Get(top);
        var dtos = _mapper.Map<List<TDto>>(entities);
        return dtos;
    }

    public virtual async Task<TDto> GetById<TDto>(int id)
    {
        var entity = await _crudService.GetById(id);
        var dto = _mapper.Map<TDto>(entity);
        return dto;
    }

    public virtual async Task<TResponseDto> Create<TResponseDto, TRequestDto>(TRequestDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity = await _crudService.Create(entity);
        return _mapper.Map<TResponseDto>(entity);
    }

    public virtual async Task<TResponseDto> Update<TResponseDto, TRequestDto>(TRequestDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity = await _crudService.Update(entity);
        return _mapper.Map<TResponseDto>(entity);
    }

    public virtual async Task<bool> Delete(int id)
    {
        return await _crudService.Delete(id);
    }
}