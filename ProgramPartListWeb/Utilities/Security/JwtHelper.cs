using System;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class JwtHelper
{
    //public static string GenerateAccessToken(string fullName, string role, int userId)
    //{
    //        var claims = new[]
    //        {
    //            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
    //            new Claim(ClaimTypes.Name, fullName),
    //            new Claim(ClaimTypes.Role, role)
    //        };

    //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["config:JwtKey"]));
    //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //        var token = new JwtSecurityToken(
    //            issuer: ConfigurationManager.AppSettings["config:JwtIssuer"],
    //            audience: ConfigurationManager.AppSettings["config:JwtAudience"],
    //            claims: claims,
    //            expires: DateTime.UtcNow.AddMinutes(30),
    //            signingCredentials: creds
    //        );

    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //}

    //public static string GenerateRefreshToken(int userId)
    //{
    //    // You can generate refresh token from a secure random string or GUID
    //    return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    //}


    private static readonly string SecretKey = ConfigurationManager.AppSettings["config:JwtKey"];
    private static readonly string RefreshSecretKey = ConfigurationManager.AppSettings["config:JwtRefreshkey"];
    private const string Issuer = "YourApp";
    private const string Audience = "YourAudience";

    public static string GenerateAccessToken(string username, string role, int userId, int expireMinutes = 60)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
            new Claim("UserId", userId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string GenerateRefreshToken(int userId, int expireDays = 30)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(RefreshSecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("UserId", userId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(expireDays),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static ClaimsPrincipal ValidateToken(string token, bool isRefreshToken = false)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token is null or empty.");

        if (token.StartsWith("Bearer "))
            token = token.Substring("Bearer ".Length).Trim();

        var segments = token.Split('.');
        if (segments.Length != 3)
            throw new SecurityTokenMalformedException($"Expected 3 segments, got {segments.Length}");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(isRefreshToken ? RefreshSecretKey : SecretKey));
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.Zero
        };

        return tokenHandler.ValidateToken(token, validationParameters, out _);
    }
}