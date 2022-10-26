using Queues.Application.Generic.DTOs;

namespace Queues.Application.Queue.DTOs;
public class QueueUpdateDto : BaseDto
{
    public string Name { get; set; } = null!;
}
