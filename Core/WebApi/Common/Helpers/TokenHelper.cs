using System.Text;
using System.Text.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CloudIn.Core.WebApi.Common.Helpers;

public static class TokenHelper
{
    public static string WriteToken(string secret, int expires, object values)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretBytes = Encoding.ASCII.GetBytes(secret);

        var claimValues = values
            .GetType()
            .GetProperties()
            .Select(
                prop => new Claim(prop.Name, prop.GetValue(values)?.ToString() ?? string.Empty)
            );

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claimValues),
            Expires = DateTime.UtcNow.AddSeconds(expires),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secretBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateJwtSecurityToken(descriptor);

        return tokenHandler.WriteToken(token);
    }

    public static ClaimsPrincipal ValidateToken(string token, string secret)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretBytes = Encoding.ASCII.GetBytes(secret);
        var signingKey = new SymmetricSecurityKey(secretBytes);

        return tokenHandler.ValidateToken(
            token,
            new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ClockSkew = TimeSpan.Zero,
            },
            out SecurityToken validatedToken
        );
    }

    public static TResult? ValidateToken<TResult>(string token, string secret)
    {
        var claimsPrincipal = TokenHelper.ValidateToken(token, secret);

        var claimValues = typeof(TResult)
            .GetProperties()
            .ToDictionary(prop => prop.Name, prop => claimsPrincipal.FindFirstValue(prop.Name));

        return JsonSerializer.Deserialize<TResult>(JsonSerializer.Serialize(claimValues));
    }
}
