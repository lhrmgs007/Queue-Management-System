using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using QMS.Core.Entities;

namespace QMS.Services.Authentication;

public interface IAuthenticationService
{
    string GenerateToken(User user);
    bool ValidateToken(string token);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly string _secretKey;
    private readonly int _expirationMinutes;

    public AuthenticationService(string secretKey, int expirationMinutes = 1440)
    {
        _secretKey = secretKey;
        _expirationMinutes = expirationMinutes;
    }

    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("RoleId", user.RoleId.ToString()),
            new Claim("RoleName", user.Role?.RoleName ?? "Unknown")
        };

        var token = new JwtSecurityToken(
            issuer: "QMS",
            audience: "QMSUsers",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var handler = new JwtSecurityTokenHandler();

            handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = true,
                ValidIssuer = "QMS",
                ValidateAudience = true,
                ValidAudience = "QMSUsers",
                ValidateLifetime = true
            }, out SecurityToken validatedToken);

            return validatedToken != null;
        }
        catch
        {
            return false;
        }
    }
}
