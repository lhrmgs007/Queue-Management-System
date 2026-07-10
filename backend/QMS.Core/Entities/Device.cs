namespace QMS.Core.Entities;

public class Device
{
    public int DeviceId { get; set; }
    public string DeviceType { get; set; } = null!;
    public string DeviceName { get; set; } = null!;
    public string? IPAddress { get; set; }
    public string Status { get; set; } = "Offline";
    public DateTime? LastHeartbeat { get; set; }
    public string Location { get; set; } = "";
    public string? FirmwareVersion { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
