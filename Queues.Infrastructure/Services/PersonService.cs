using AutoMapper;
using Queues.Application.Interfaces;
using Queues.Domain.Entities;
using Queues.Infrastructure.Context;

namespace Queues.Infrastructure.Services;
public class PersonService : BaseCrudService<Person>, IPersonService
{
    public PersonService(IQueuesDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}
