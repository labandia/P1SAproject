using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProgramPartListWeb.Utilities.Security
{
    public sealed class JWTAuthentication
    {
        private static readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        public static class JwtConfig
        {
            public static string Issuer =>
                ConfigurationManager.AppSettings["config:JwtIssuer"]?.Trim();

            public static string Audience =>
                ConfigurationManager.AppSettings["config:JwtAudience"]?.Trim();

            public static string AccessSecretKey =>
                ConfigurationManager.AppSettings["config:JwtKey"]?.Trim();

            public static string RefreshSecretKey =>
                ConfigurationManager.AppSettings["config:RefreshSecretKey"]?.Trim();

            public static int ExpireMinutes =>
                int.Parse(ConfigurationManager.AppSettings["config:JwtExpireMinutes"]);

            public static int RefreshExpireDays =>
                int.Parse(ConfigurationManager.AppSettings["config:JwtExpireDays"]);
        }

        private static SymmetricSecurityKey GetKey(string secret)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        }

        /* =========================================================
           ACCESS TOKEN GENERATION
           ========================================================= */
        public static string GenerateAccessToken(string fullName, string role, int userId)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, fullName),
            new Claim(ClaimTypes.Role, role),

            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            new Claim(
                JwtRegisteredClaimNames.Iat,
                DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64)
        };

            var key = GetKey(JwtConfig.AccessSecretKey);
         
            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(JwtConfig.ExpireMinutes);

            var token = new JwtSecurityToken(
                issuer: JwtConfig.Issuer,
                audience: JwtConfig.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: creds
            );

            return tokenHandler.WriteToken(token);
        }

        /* =========================================================
           REFRESH TOKEN GENERATION
           ========================================================= */
        public static string GenerateRefreshToken(string fullName, string role, int userId)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim("fullname", fullName),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            new Claim(
                JwtRegisteredClaimNames.Iat,
                DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64)
        };

            var key = GetKey(JwtConfig.RefreshSecretKey);

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddDays(JwtConfig.RefreshExpireDays);

            var token = new JwtSecurityToken(
                issuer: JwtConfig.Issuer,
                audience: JwtConfig.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: creds
            );

            return tokenHandler.WriteToken(token);
        }

        /* =========================================================
           ACCESS TOKEN VALIDATION
           ========================================================= */
        public static ClaimsPrincipal ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token is null or empty.");

            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                token = token.Substring("Bearer ".Length).Trim();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = JwtConfig.Issuer,
                ValidAudience = JwtConfig.Audience,

                IssuerSigningKey = GetKey(JwtConfig.AccessSecretKey),

                RequireSignedTokens = true,
                RequireExpirationTime = true,

                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }

        /* =========================================================
           REFRESH TOKEN VALIDATION
           ========================================================= */
        public static ClaimsPrincipal ValidateRefreshToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return null;

            if (refreshToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                refreshToken = refreshToken.Substring("Bearer ".Length).Trim();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = JwtConfig.Issuer,
                ValidAudience = JwtConfig.Audience,

                IssuerSigningKey = GetKey(JwtConfig.RefreshSecretKey),

                RequireSignedTokens = true,
                RequireExpirationTime = true,

                ClockSkew = TimeSpan.Zero
            };

            try
            {
                return tokenHandler.ValidateToken(refreshToken, validationParameters, out _);
            }
            catch
            {
                return null;
            }
        }
    }
}