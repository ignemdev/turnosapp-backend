using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Queues.Application.Generic.Handlers;
using Queues.Application.Generic.Interfaces;
using Queues.Application.Interfaces;
using Queues.Application.Person.Extensions;
using Queues.Application.PersonQueue.DTOs;
using Queues.Domain.Enumerables;

namespace Queues.Application.PersonQueue.Handlers
{
    public interface IPersonQueueHandler : IBaseCrudHandler<Domain.Entities.PersonQueue>
    {
        Task<PersonQueueDetailDto> Create(int queueId, PersonQueueAddDto dto);
        Task<List<PersonQueueDetailDto>> GetAll();
        Task<List<PersonQueueDetailDto>> GetAllPeopleQueueByQueueId(int queueId);
        Task<PersonQueueDetailDto> GetById(int id);
        Task<PersonQueueDetailDto> GetNextPersonQueueByQueueId(int queueId);
        Task<bool> IsPersonInQueue(int personId, int queueId);
        Task<PersonQueueDetailDto> SetActivePersonQueueByQueueId(int queueId);
        Task<PersonQueueDetailDto> GetActivePersonQueueByQueueId(int queueId);
        Task<PersonQueueDetailDto> SetPersonQueueAsAttendedByQueueId(int queueId);
        Task<int> GetPeopleQueuesCountByQueueId(int queueId);
    }

    public class PersonQueueHandler : BaseCrudHandler<Domain.Entities.PersonQueue>, IPersonQueueHandler
    {
        private readonly IMapper _mapper;
        private readonly IPersonQueueService _personQueueService;
        private readonly IPersonService _personService;
        public PersonQueueHandler(
            IPersonQueueService personQueueService,
            IPersonService personService,
            IMapper mapper) : base(personQueueService, mapper)
        {
            _personQueueService = personQueueService;
            _personService = personService;
            _mapper = mapper;
        }

        public async Task<PersonQueueDetailDto> Create(int queueId, PersonQueueAddDto dto)
        {
            var personQueue = _mapper.Map<Domain.Entities.PersonQueue>(dto);
            var personId = personQueue.PersonId;

            var isPersonInQueue = await IsPersonInQueue(personId, queueId);
            if (isPersonInQueue)
                throw new InvalidOperationException("La persona ya esta en cola.");

            var person = await _personService.GetById(personId);
            if (person == default)
                throw new InvalidOperationException("La persona no existe.");

            personQueue.PreferenceLevel = person.GetPreferenceLevel();
            personQueue.QueueId = queueId;

            var createdPersonQueue = await _personQueueService.Create(personQueue);
            var personQueueDetail = _mapper.Map<PersonQueueDetailDto>(createdPersonQueue);

            return personQueueDetail;
        }

        public async Task<PersonQueueDetailDto> GetActivePersonQueueByQueueId(int queueId)
        {
            var queryResult = _personQueueService.Query();

            var activePersonQueue = await queryResult
                .AsNoTracking()
                .Include(q => q.Person)
                .FirstOrDefaultAsync(q => q.QueueId == queueId && q.State == State.Active);

            var personQueueDetail = _mapper.Map<PersonQueueDetailDto>(activePersonQueue);

            return personQueueDetail;
        }

        public async Task<List<PersonQueueDetailDto>> GetAll()
        {
            var inQueueStates = new HashSet<State> { State.InQueue, State.Active };

            var peopleQueues = await _personQueueService.Query()
                .AsNoTracking()
                .Take(1000)
                .Where(q => inQueueStates.Contains(q.State))
                .Include(q => q.Person)
                .Include(q => q.Queue)
                .OrderBy(q => q.PreferenceLevel)
                .ThenBy(q => q.ArrivedTime)
                .ToListAsync();

            var peopleQueuesDetails = _mapper.Map<List<PersonQueueDetailDto>>(peopleQueues);

            return peopleQueuesDetails;
        }

        //TODO: organizar por preferencia
        public async Task<List<PersonQueueDetailDto>> GetAllPeopleQueueByQueueId(int queueId)
        {
            var inQueueStates = new HashSet<State> { State.InQueue, State.Active };

            var peopleQueues = await _personQueueService.Query()
                .AsNoTracking()
                .Take(1000)
                .Where(q => q.QueueId == queueId && inQueueStates.Contains(q.State))
                .Include(q => q.Person)
                .OrderBy(q => q.PreferenceLevel)
                .ThenBy(q => q.ArrivedTime)
                .ToListAsync();

            var peopleQueuesDetails = _mapper.Map<List<PersonQueueDetailDto>>(peopleQueues);

            return peopleQueuesDetails;
        }

        public async Task<PersonQueueDetailDto> GetById(int id)
        {
            var queryResult = _personQueueService.Query();

            var personQueue = await queryResult
                .Include(q => q.Queue)
                .Include(q => q.Person)
                .FirstOrDefaultAsync(q => q.Id == id);

            var personQueueDetail = _mapper.Map<PersonQueueDetailDto>(personQueue);

            return personQueueDetail;
        }

        public async Task<PersonQueueDetailDto> GetNextPersonQueueByQueueId(int queueId)
        {
            var nextPersonQueue = default(PersonQueueDetailDto);
            var peopleInQueue = await GetAllPeopleQueueByQueueId(queueId);

            if (!peopleInQueue.Any())
                return nextPersonQueue;

            nextPersonQueue = peopleInQueue.FirstOrDefault();

            return nextPersonQueue;
        }

        public async Task<int> GetPeopleQueuesCountByQueueId(int queueId)
        {
            var peopleQueuesCount = await _personQueueService.Query()
                .AsNoTracking()
                .Take(1000)
                .Where(q => q.QueueId == queueId && q.State == State.InQueue)
                .CountAsync();

            return peopleQueuesCount;
        }

        public async Task<bool> IsPersonInQueue(int personId, int queueId)
        {
            var peopleInQueue = await GetAllPeopleQueueByQueueId(queueId);

            var isPersonInQueue = peopleInQueue.Any(
                q => q.PersonId == personId
            );

            return isPersonInQueue;
        }

        public async Task<PersonQueueDetailDto> SetActivePersonQueueByQueueId(int queueId)
        {
            var nextPersonQueue = await GetNextPersonQueueByQueueId(queueId);

            if (nextPersonQueue == default)
                throw new InvalidOperationException("No hay mas personas en cola.");

            nextPersonQueue.State = (int)State.Active;

            return await base.Update<PersonQueueDetailDto, PersonQueueDetailDto>(nextPersonQueue);
        }

        public async Task<PersonQueueDetailDto> SetPersonQueueAsAttendedByQueueId(int queueId)
        {
            var activePersonQueue = await GetActivePersonQueueByQueueId(queueId);

            if (activePersonQueue == default)
                return default(PersonQueueDetailDto);

            activePersonQueue.State = (int)State.Attended;

            return await base.Update<PersonQueueDetailDto, PersonQueueDetailDto>(activePersonQueue);
        }


        //GetQueueQuantity(int queueId)
    }
}
