using AutoMapper;
using Queues.Application.Generic.Handlers;
using Queues.Application.Generic.Interfaces;
using Queues.Application.Interfaces;
using Queues.Application.Queue.DTOs;

namespace Queues.Application.Queue.Handlers
{

    public interface IQueueHandler : IBaseCrudHandler<Domain.Entities.Queue>
    {
        Task<List<QueueDetailDto>> GetAll();
        Task<QueueDetailDto> GetById(int id);
        Task<QueueDetailDto> Update(QueueUpdateDto dto);
        Task<QueueDetailDto> Create(QueueAddDto dto);
    }
    public class QueueHandler : BaseCrudHandler<Domain.Entities.Queue>, IQueueHandler
    {

        private readonly IQueueService _crudService;
        private readonly IMapper _mapper;

        public QueueHandler(IQueueService crudService, IMapper mapper) : base(crudService, mapper)
        {
            _crudService = crudService;
            _mapper = mapper;
        }
        public async Task<List<QueueDetailDto>> GetAll()
        {
            return await base.GetAll<QueueDetailDto>();
        }
        public async Task<QueueDetailDto> GetById(int id)
        {
            return await base.GetById<QueueDetailDto>(id);
        }

        public async Task<QueueDetailDto> Create(QueueAddDto dto)
        {
            return await base.Create<QueueDetailDto, QueueAddDto>(dto);
        }

        public async Task<QueueDetailDto> Update(QueueUpdateDto dto)
        {
            return await base.Update<QueueDetailDto, QueueUpdateDto>(dto);
        }
    }
}
