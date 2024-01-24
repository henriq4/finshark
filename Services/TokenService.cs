using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using finshark.Interfaces;
using finshark.Models;
using Microsoft.IdentityModel.Tokens;

namespace finshark.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;
    
    public TokenService(IConfiguration configuration)
    {
        this._configuration = configuration;
        this._key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigninKey"]));
    }
    
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            Expires = DateTime.Now.AddDays(14)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}