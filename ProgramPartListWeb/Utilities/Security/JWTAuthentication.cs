using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProgramPartListWeb.Utilities.Security
{
    public class JWTAuthentication
    {
        private static string RefreshSecretKey =>
        ConfigurationManager.AppSettings["config:RefreshSecretKey"];

        private static string SecretKey =>
            ConfigurationManager.AppSettings["config:JwtKey"];

        private static string Issuer =>
            ConfigurationManager.AppSettings["config:JwtIssuer"];

        private static string Audience =>
            ConfigurationManager.AppSettings["config:JwtAudience"];


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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["config:JwtKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["config:JwtExpireMinutes"] ?? "15"));

            var token = new JwtSecurityToken(
                          issuer: ConfigurationManager.AppSettings["config:JwtIssuer"],
                          audience: ConfigurationManager.AppSettings["config:JwtAudience"],
                          claims: claims,
                          expires: expires,
                          signingCredentials: creds);
            // Optional: Add kid header for key rotation
            token.Header["kid"] = "current-key-id"; // Replace with your actual key ID

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public static string GenerateRefreshToken(int userId, int expireDays = 30)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(RefreshSecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
                    {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
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

            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                token = token.Substring("Bearer ".Length).Trim();

            var segments = token.Split('.');
            if (segments.Length != 3)
                throw new SecurityTokenMalformedException($"Expected 3 segments, got {segments.Length}");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(isRefreshToken ? RefreshSecretKey : SecretKey));

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



        public static ClaimsPrincipal ValidateRefreshToken(string refreshToken)
        {
            refreshToken = refreshToken.Replace("Bearer ", "").Trim();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(RefreshSecretKey);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Issuer,
                ValidateAudience = true,
                ValidAudience = Audience,
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