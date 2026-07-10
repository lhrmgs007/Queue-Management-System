namespace QMS.Core.Entities;

public class Ticket
{
    public int TicketId { get; set; }
    public int QueueId { get; set; }
    public string TicketNumber { get; set; } = null!;
    public int ServiceId { get; set; }
    public string Status { get; set; } = "Waiting";
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiryDate { get; set; }
    public int? CounterId { get; set; }
    public int CallCount { get; set; } = 0;

    public virtual Queue? Queue { get; set; }
    public virtual Service? Service { get; set; }
    public virtual ICollection<TicketHistory> Histories { get; set; } = new List<TicketHistory>();
    public virtual ICollection<TicketCall> Calls { get; set; } = new List<TicketCall>();
}
