using SistemaCreditos.Application.DTOs.Usuarios;
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Application.Interfaces.Security;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.DTOs.Auth
{
    public class LoginRequestDto
    {
        public string UsernameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
