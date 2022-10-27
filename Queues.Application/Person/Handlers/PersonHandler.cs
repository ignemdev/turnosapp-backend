using AutoMapper;
using Queues.Application.Generic.Handlers;
using Queues.Application.Generic.Interfaces;
using Queues.Application.Interfaces;
using Queues.Application.Person.DTOs;

namespace Queues.Application.Person.Handlers
{
    public interface IPersonHandler : IBaseCrudHandler<Domain.Entities.Person>
    {
        Task<PersonDetailDto> GetById(int id);
        Task<PersonDetailDto> Create(PersonAddDto dto);
    }

    public class PersonHandler : BaseCrudHandler<Domain.Entities.Person>, IPersonHandler
    {
        public PersonHandler(IPersonService crudService, IMapper mapper) : base(crudService, mapper)
        {
        }

        public Task<PersonDetailDto> Create(PersonAddDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonDetailDto> GetById(int id)
        {
            return await base.GetById<PersonDetailDto>(id);
        }
    }
}
