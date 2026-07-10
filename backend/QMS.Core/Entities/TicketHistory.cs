namespace QMS.Core.Entities;

public class TicketHistory
{
    public int HistoryId { get; set; }
    public int TicketId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime StatusChangedDate { get; set; } = DateTime.UtcNow;
    public int? CounterId { get; set; }
    public string? Notes { get; set; }

    public virtual Ticket? Ticket { get; set; }
    public virtual Counter? Counter { get; set; }
}
