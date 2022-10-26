using Queues.Application.Generic.DTOs;
using Queues.Application.Person.DTOs;
using Queues.Application.Queue.DTOs;

namespace Queues.Application.PersonQueue.DTOs;
public class PersonQueueDetailDto : BaseDto
{
    public int PersonId { get; set; }
    public int QueueId { get; set; }
    public int State { get; set; }
    public int PreferenceLevel { get; set; }
    public DateTime ArrivedTime { get; set; }
    public PersonDetailDto? Person { get; set; } = null!;
    public QueueDetailDto? Queue { get; set; } = null!;
}
