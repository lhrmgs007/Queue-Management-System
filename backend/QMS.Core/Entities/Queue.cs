namespace QMS.Core.Entities;

public class Queue
{
    public int QueueId { get; set; }
    public string QueueName { get; set; } = null!;
    public string Prefix { get; set; } = null!;
    public string Color { get; set; } = "#3b82f6";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public int CurrentTicketNumber { get; set; } = 0;
    public int NextTicketNumber { get; set; } = 1;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
