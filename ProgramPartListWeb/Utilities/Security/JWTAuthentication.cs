using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProgramPartListWeb.Utilities.Security
{
    public class JWTAuthentication
    {
  
        public static class JwtConfig
        {
            public static string Issuer => ConfigurationManager.AppSettings["config:JwtIssuer"];
            public static string Audience => ConfigurationManager.AppSettings["config:JwtAudience"];
            public static string AccessSecretKey => ConfigurationManager.AppSettings["config:JwtKey"];
            public static string RefreshSecretKey => ConfigurationManager.AppSettings["config:RefreshSecretKey"];
            public static int ExpireMinutes => int.Parse(ConfigurationManager.AppSettings["config:JwtExpireMinutes"]);
            public static int RefreshExpireDays => int.Parse(ConfigurationManager.AppSettings["config:JwtExpireDays"]);
        }


        public static string GenerateAccessToken(string fullName, string role, int userId)
        {
            // Store To Claims Like an Mail Envelope that is Seal in information
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),       
                new Claim(JwtRegisteredClaimNames.UniqueName, fullName),         
                new Claim(ClaimTypes.Role, role),                            
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.AccessSecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(JwtConfig.ExpireMinutes));

            var token = new JwtSecurityToken(
                          issuer: JwtConfig.Issuer,
                          audience: JwtConfig.Audience,
                          claims: claims,
                          expires: expires,
                          signingCredentials: creds);
            // Optional: Add kid header for key rotation
            token.Header["kid"] = "current-key-id"; // Replace with your actual key ID

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public static string GenerateRefreshToken(string fullName, string role, int userId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim("Fullname", fullName),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.RefreshSecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(JwtConfig.RefreshExpireDays));

            var token = new JwtSecurityToken(
                issuer: JwtConfig.Issuer,
                audience: JwtConfig.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static ClaimsPrincipal ValidateToken(string token, bool isRefreshToken = false)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token is null or empty.");

            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                token = token.Substring("Bearer ".Length).Trim();

            var segments = token.Split('.');
            if (segments.Length != 3)
                throw new SecurityTokenMalformedException($"Expected 3 segments, got {segments.Length}");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(isRefreshToken ? JwtConfig.RefreshSecretKey : JwtConfig.AccessSecretKey));

            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = JwtConfig.Issuer,
                ValidAudience = JwtConfig.Audience,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }



        public static ClaimsPrincipal ValidateRefreshToken(string refreshToken)
        {
            //Debug.WriteLine($@"Issure : {JwtConfig.Issuer}");
            //Debug.WriteLine($@"Audience : {JwtConfig.Audience}");
            //Debug.WriteLine($@"AccessSecretKey :  {JwtConfig.AccessSecretKey}");
            //Debug.WriteLine($@"RefreshSecretKey :    {JwtConfig.RefreshSecretKey}");
            //Debug.WriteLine($@"ExpireMinutes :  {JwtConfig.ExpireMinutes}");
            //Debug.WriteLine($@"RefreshExpireDays :  {JwtConfig.RefreshExpireDays}");

            refreshToken = refreshToken.Replace("Bearer ", "").Trim();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(JwtConfig.RefreshSecretKey);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = JwtConfig.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                return tokenHandler.ValidateToken(refreshToken, parameters, out _);
            }
            catch
            {
                return null;
            }
        }
    }
}