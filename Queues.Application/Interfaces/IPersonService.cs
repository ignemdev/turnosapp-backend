using Queues.Application.Generic.Interfaces;
using Queues.Domain.Entities;

namespace Queues.Application.Interfaces;
public interface IPersonService : IBaseCrudService<Person>
{
}
