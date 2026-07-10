namespace QMS.Core.Entities;

public class SystemSettings
{
    public int SettingId { get; set; }
    public string SettingKey { get; set; } = null!;
    public string SettingValue { get; set; } = null!;
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
}
