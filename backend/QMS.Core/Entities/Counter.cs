namespace QMS.Core.Entities;

public class Counter
{
    public int CounterId { get; set; }
    public string CounterName { get; set; } = null!;
    public string Status { get; set; } = "Closed";
    public int ServiceId { get; set; }
    public string Location { get; set; } = "";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? StatusChangedDate { get; set; }
    public int? CurrentTicketId { get; set; }

    public virtual Service? Service { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<TicketHistory> TicketHistories { get; set; } = new List<TicketHistory>();
    public virtual ICollection<TicketCall> TicketCalls { get; set; } = new List<TicketCall>();
}
