using AutoMapper;
using Queues.Application.Interfaces;
using Queues.Domain.Entities;
using Queues.Infrastructure.Context;

namespace Queues.Infrastructure.Services;
public class PersonQueueService : BaseCrudService<PersonQueue>, IPersonQueueService
{
    public PersonQueueService(IQueuesDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}
