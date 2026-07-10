namespace QMS.Core.Entities;

public class AuditLog
{
    public int LogId { get; set; }
    public int? UserId { get; set; }
    public string Action { get; set; } = null!;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Details { get; set; } = "";
    public string? IPAddress { get; set; }

    public virtual User? User { get; set; }
}
