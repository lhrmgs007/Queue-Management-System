namespace QMS.Core.Entities;

public class RolePermission
{
    public int RolePermissionId { get; set; }
    public int RoleId { get; set; }
    public string PermissionKey { get; set; } = null!;
    public string PermissionName { get; set; } = null!;

    public virtual Role? Role { get; set; }
}
