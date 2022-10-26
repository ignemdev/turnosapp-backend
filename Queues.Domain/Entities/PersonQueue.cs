using Queues.Domain.Enumerables;

namespace Queues.Domain.Entities;
public class PersonQueue : BaseEntity
{
    public int PersonId { get; set; }
    public int QueueId { get; set; }
    public State State { get; set; } = State.InQueue;
    public PreferenceLevel PreferenceLevel { get; set; } = PreferenceLevel.ThirdLevel;
    public DateTime ArrivedTime { get; set; }
    public Person Person { get; set; } = null!;
    public Queue Queue { get; set; } = null!;
}
