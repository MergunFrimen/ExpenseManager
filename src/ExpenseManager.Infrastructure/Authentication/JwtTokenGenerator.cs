using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using ExpenseManager.Application.Common.Interfaces.Authentication;
using ExpenseManager.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseManager.Infrastructure.Authentication;

public class JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettingsOptions)
    : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettingsOptions = jwtSettingsOptions.Value;

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettingsOptions.Secret)
            ),
            SecurityAlgorithms.HmacSha256
        );

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            new Claim(JwtRegisteredClaimNames.Iss, _jwtSettingsOptions.Issuer),
            new Claim(JwtRegisteredClaimNames.Aud, _jwtSettingsOptions.Audience),
        };

        var jwtSecurityToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: dateTimeProvider.UtcNow.AddMinutes(_jwtSettingsOptions.ExpiryMinutes)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}