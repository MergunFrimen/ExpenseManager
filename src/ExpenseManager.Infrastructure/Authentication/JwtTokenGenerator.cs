using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseManager.Application.Common.Interfaces.Authentication;
using ExpenseManager.Application.Common.Interfaces.Services;
using ExpenseManager.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseManager.Infrastructure.Authentication;

public class JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettingsOptions)
    : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettingsOptions = jwtSettingsOptions.Value;

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettingsOptions.Secret)
            ),
            SecurityAlgorithms.HmacSha256
        );

        Claim[] claims =
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString() ?? string.Empty),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iss, _jwtSettingsOptions.Issuer),
            new(JwtRegisteredClaimNames.Aud, _jwtSettingsOptions.Audience)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: dateTimeProvider.UtcNow.AddMinutes(_jwtSettingsOptions.ExpiryMinutes)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}