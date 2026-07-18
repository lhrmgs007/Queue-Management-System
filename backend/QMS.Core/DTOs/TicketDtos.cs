namespace QMS.Core.DTOs;

public class TicketDto
{
    public int TicketId { get; set; }
    public string TicketNumber { get; set; } = null!;
    public int QueueId { get; set; }
    public string QueueName { get; set; } = null!;
    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public int CallCount { get; set; }
}

public class CreateTicketRequest
{
    public int QueueId { get; set; }
    public int ServiceId { get; set; }
}

public class UpdateTicketStatusRequest
{
    public int TicketId { get; set; }
    public string Status { get; set; } = null!;
}
