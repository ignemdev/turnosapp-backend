using Queues.Application.Generic.DTOs;
using Queues.Domain.Entities;

namespace Queues.Application.Generic.Interfaces;
public interface IBaseCrudHandler<TDto, TEntity> where TDto : BaseDto where TEntity : BaseEntity
{
    Task<IQueryable<TEntity>> Query();
    Task<TDto> GetById(int id);
    Task<TDto> Create(TDto dto);
    Task<TDto> Update(TDto dto);
    Task<TDto> Update(int id, TDto dto);
    Task<bool> Delete(int id);
}
