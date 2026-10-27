using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EntitiesLayer;
using Microsoft.IdentityModel.Tokens;

namespace PresentationLayer.Security
{
    public sealed class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler = new();
        private readonly SigningCredentials _signingCredentials;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtTokenService(IConfiguration configuration)
        {
            var signingKey = configuration["Jwt:SigningKey"];
            if (string.IsNullOrWhiteSpace(signingKey) || Encoding.UTF8.GetByteCount(signingKey) < 32)
            {
                throw new InvalidOperationException(
                    "Configure Jwt:SigningKey con una clave aleatoria de al menos 32 bytes mediante configuración segura.");
            }

            _issuer = configuration["Jwt:Issuer"] ?? "PetFinderApi";
            _audience = configuration["Jwt:Audience"] ?? "PetFinderApi";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        public string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new(ClaimTypes.Name, usuario.Nombre),
                new(ClaimTypes.Email, usuario.Correo),
                new(ClaimTypes.Role, usuario.EsAdministrador ? "Admin" : "User")
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: _signingCredentials);

            return _tokenHandler.WriteToken(token);
        }
    }
}
