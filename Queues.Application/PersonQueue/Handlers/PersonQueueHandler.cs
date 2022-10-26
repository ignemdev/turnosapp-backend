using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Queues.Application.Generic.Handlers;
using Queues.Application.Generic.Interfaces;
using Queues.Application.Interfaces;
using Queues.Application.PersonQueue.DTOs;
using Queues.Domain.Enumerables;

namespace Queues.Application.PersonQueue.Handlers
{
    public interface IPersonQueueHandler : IBaseCrudHandler<Domain.Entities.PersonQueue>
    {
        Task<List<PersonQueueDetailDto>> GetAll();
        Task<PersonQueueDetailDto> GetById(int id);
        Task<PersonQueueDetailDto> Create(PersonQueueAddDto dto);
        Task<bool> IsPersonInQueue(int personId, int queueId);
    }

    public class PersonQueueHandler : BaseCrudHandler<Domain.Entities.PersonQueue>, IPersonQueueHandler
    {
        private readonly IPersonQueueService _crudService;
        private readonly IMapper _mapper;

        public PersonQueueHandler(IPersonQueueService crudService, IMapper mapper) : base(crudService, mapper)
        {
            _crudService = crudService;
            _mapper = mapper;
        }
        //TODO:determinar nivel de preferencia
        public async Task<PersonQueueDetailDto> Create(PersonQueueAddDto dto)
        {
            var personQueue = _mapper.Map<Domain.Entities.PersonQueue>(dto);

            personQueue.ArrivedTime = DateTime.UtcNow;

            //check si persona esta en la misma cola
            var isPersonInQueue = await IsPersonInQueue(dto.PersonId, dto.QueueId);
            if (isPersonInQueue)
                throw new InvalidOperationException("La persona ya esta en cola.");

            throw new NotImplementedException();
        }
        //TODO: organizar por preferencia
        public async Task<List<PersonQueueDetailDto>> GetAll()
        {
            var inQueueStates = new HashSet<State> { State.InQueue, State.Active };

            var queues = await _crudService.Query()
                .Take(1000)
                .Where(q => inQueueStates.Contains(q.State))
                .OrderBy(q => q.ArrivedTime)
                .ToListAsync();

            var queuesDtos = _mapper.Map<List<PersonQueueDetailDto>>(queues);

            return queuesDtos;
        }

        public Task<PersonQueueDetailDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsPersonInQueue(int personId, int queueId)
        {
            var peopleInQueue = await GetAll();

            var isPersonInQueue = peopleInQueue.Any(
                q => q.PersonId == personId
            );

            return isPersonInQueue;
        }
    }
}
