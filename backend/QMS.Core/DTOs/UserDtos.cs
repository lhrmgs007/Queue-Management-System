namespace QMS.Core.DTOs;

public class LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginResponse
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string RoleName { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}

public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;
    public int? CounterId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
}
