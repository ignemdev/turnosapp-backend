using AutoMapper;
using Queues.Application.Interfaces;
using Queues.Domain.Entities;
using Queues.Infrastructure.Context;

namespace Queues.Infrastructure.Services;
public class QueueService : BaseCrudService<Queue>, IQueueService
{
    public QueueService(IQueuesDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}
