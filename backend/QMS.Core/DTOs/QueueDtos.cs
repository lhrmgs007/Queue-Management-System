namespace QMS.Core.DTOs;

public class QueueDto
{
    public int QueueId { get; set; }
    public string QueueName { get; set; } = null!;
    public string Prefix { get; set; } = null!;
    public string Color { get; set; } = null!;
    public bool IsActive { get; set; }
    public int CurrentTicketNumber { get; set; }
    public int NextTicketNumber { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class CreateQueueRequest
{
    public string QueueName { get; set; } = null!;
    public string Prefix { get; set; } = null!;
    public string Color { get; set; } = "#3b82f6";
}
