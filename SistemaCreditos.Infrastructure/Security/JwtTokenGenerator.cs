using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaCreditos.Application.Interfaces.Security;
using SistemaCreditos.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaCreditos.Infrastructure.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarToken(Usuario usuario)
        {
            // 1. Traer la clave secreta y convertirla a bytes
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

            // 2. Elegir el algoritmo de encriptación
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Crear los 'Claims' (Los datos públicos que viajan dentro del token)
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(ClaimTypes.Role, usuario.Rol?.Nombre ?? "SinRol")
            };

            // 4. Armar la configuración completa del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryMinutes"]!)),
                SigningCredentials = credentials,
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            // 5. Generar y devolver el token como un texto
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}