using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Common.Security.APIs
{ 
    public class TokenService
    {
        private const string SecretKey = "your_secret_key_here"; //یک کلید امن و پیچیده 
        private readonly SymmetricSecurityKey _signingKey;

        public TokenService()
        {
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        }

        public string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, username)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
