namespace QMS.Core.DTOs;

public class CounterDto
{
    public int CounterId { get; set; }
    public string CounterName { get; set; } = null!;
    public string Status { get; set; } = null!;
    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = null!;
    public string Location { get; set; } = null!;
    public bool IsActive { get; set; }
    public int? CurrentTicketId { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class CreateCounterRequest
{
    public string CounterName { get; set; } = null!;
    public int ServiceId { get; set; }
    public string Location { get; set; } = "";
}

public class UpdateCounterStatusRequest
{
    public int CounterId { get; set; }
    public string Status { get; set; } = null!;
}
