using Microsoft.IdentityModel.Tokens;
using RiichiGang.Domain;
using RiichiGang.Service.Settings;
using System;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RiichiGang.Service
{
    public class AuthenticationService
    {
        private readonly AuthenticationSettings _settings;

        public AuthenticationService(AuthenticationSettings settings)
        {
            _settings = settings;
        }

        public string Authenticate(User user, string password)
        {
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new ArgumentException("Senha invalida");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}