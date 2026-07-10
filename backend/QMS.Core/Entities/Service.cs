namespace QMS.Core.Entities;

public class Service
{
    public int ServiceId { get; set; }
    public int QueueId { get; set; }
    public string ServiceName { get; set; } = null!;
    public int AverageServiceTime { get; set; } = 300;
    public string Description { get; set; } = "";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public virtual Queue? Queue { get; set; }
    public virtual ICollection<Counter> Counters { get; set; } = new List<Counter>();
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
