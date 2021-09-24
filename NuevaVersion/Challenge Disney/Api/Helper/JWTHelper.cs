using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Api.Helper
{
    public class JWTHelper
    {
        private readonly byte[] Secret;

        public JWTHelper (string SecretKey)
        {
            this.Secret = Encoding.ASCII.GetBytes(@SecretKey);
        }

        public string CreateToken(string @username)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, @username));

            var TokenDescription = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(this.Secret),SecurityAlgorithms.HmacSha256Signature)
            };
            var TokenHandler = new JwtSecurityTokenHandler();
            var CreatedToken = TokenHandler.CreateToken(TokenDescription);
            
            return TokenHandler.WriteToken(CreatedToken);
        }
    }
}