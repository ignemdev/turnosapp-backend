using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Queues.Application.Generic.Handlers;
using Queues.Application.Generic.Interfaces;
using Queues.Application.Interfaces;
using Queues.Application.Person.DTOs;
using Queues.Application.Person.Models;

namespace Queues.Application.Person.Handlers
{
    public interface IPersonHandler : IBaseCrudHandler<Domain.Entities.Person>
    {
        Task<PersonDetailDto> GetById(int id);
        Task<PersonDetailDto> Create(PersonDocumentModel personDocument, PersonAddDto dto);

        Task<bool> PersonIdentificationNumberExists(string identificationNumber);
    }

    public class PersonHandler : BaseCrudHandler<Domain.Entities.Person>, IPersonHandler
    {
        private readonly IPersonService _crudService;
        private readonly IMapper _mapper;


        public PersonHandler(IPersonService crudService, IMapper mapper) : base(crudService, mapper)
        {
            _crudService = crudService;
            _mapper = mapper;
        }

        public async Task<PersonDetailDto> Create(PersonDocumentModel personDocument, PersonAddDto dto)
        {
            var identificationNumberExists = await PersonIdentificationNumberExists(personDocument.IdentificationNumber);

            if (identificationNumberExists)
                throw new InvalidOperationException("La cedula ya existe.");

            var person = _mapper.Map<Domain.Entities.Person>(dto);

            person.Name = personDocument.Name;
            person.LastName = personDocument.LastName;
            person.IdentificationNumber = personDocument.IdentificationNumber;
            person.Gender = personDocument.Gender;

            var createdPerson = await _crudService.Create(person);

            var personDetail = _mapper.Map<PersonDetailDto>(createdPerson);

            return personDetail;
        }

        public async Task<PersonDetailDto> GetById(int id)
        {
            return await base.GetById<PersonDetailDto>(id);
        }

        public async Task<bool> PersonIdentificationNumberExists(string identificationNumber)
        {
            if (string.IsNullOrWhiteSpace(identificationNumber))
                return default;

            var queryResult = _crudService.Query();

            var identificationNumberExists =
                await queryResult.AnyAsync(p => p.IdentificationNumber == identificationNumber);

            return identificationNumberExists;
        }
    }
}
