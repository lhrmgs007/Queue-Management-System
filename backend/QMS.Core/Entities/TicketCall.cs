namespace QMS.Core.Entities;

public class TicketCall
{
    public int CallId { get; set; }
    public int TicketId { get; set; }
    public int CounterId { get; set; }
    public DateTime CalledDate { get; set; } = DateTime.UtcNow;
    public int DurationSeconds { get; set; } = 0;
    public string Result { get; set; } = "Pending";

    public virtual Ticket? Ticket { get; set; }
    public virtual Counter? Counter { get; set; }
}
