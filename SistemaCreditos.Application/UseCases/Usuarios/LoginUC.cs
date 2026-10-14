using SistemaCreditos.Application.DTOs.Auth;
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Application.Interfaces.Security;

namespace SistemaCreditos.Application.UseCases.Usuarios
{
    public class LoginUC
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUC(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenGenerator JwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = JwtTokenGenerator;
        }

        public async Task<LoginResponseDto> EjecutarAsync(LoginRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UsernameOrEmail) || string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new ValidationException($"El nombre de usuario o correo electrónico y la contraseña son obligatorios.");
            }
            var usuario = await _unitOfWork.Usuarios.GetByUsernameOrEmailAsync(dto.UsernameOrEmail);
            
            if (usuario == null)
            {
                throw new ValidationException("Credenciales incorrectas");
            }
            if (!_passwordHasher.VerifyPassword(dto.Password, usuario.Password))
            {
                throw new ValidationException("Credenciales incorrectas");
            }
            if (!usuario.Estado)
            {
                throw new ValidationException("La cuenta del usuario está desactivada.");
            }
            
            var token = _jwtTokenGenerator.GenerarToken(usuario);

            return new LoginResponseDto
            {
                Token = token,
                Username = usuario.UserName,
                NombreCompleto = usuario.ObtenerNombreCompleto(),
                Email = usuario.Email,
                Rol = usuario.Rol?.Nombre ?? "Sin rol",
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }
    }    
}
