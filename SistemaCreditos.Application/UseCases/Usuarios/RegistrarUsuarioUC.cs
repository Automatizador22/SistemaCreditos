using SistemaCreditos.Application.DTOs.Usuarios;
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Application.Interfaces.Security;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.UseCases.Usuarios
{
    public class RegistrarUsuarioUC
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public RegistrarUsuarioUC(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsuarioResponseDto> EjecutarAsync(RegistrarUsuarioRequestDto dto)
        {
            //Verificar que el email no exista
            if (await _unitOfWork.Usuarios.ExistsEmailAsync(dto.Email))
            {
                throw new ValidationException($"El correo '{dto.Email}' ya está registrado.");
            }

            // Verificar que el username no exista
            if (await _unitOfWork.Usuarios.ExistsUsernameAsync(dto.Username))
            {
                throw new ValidationException($"El usuario '{dto.Username}' ya está en uso.");
            }

            // Crear la entidad principal
            var nuevoUsuario = new Usuario
            {
                TipoDocumento = dto.TipoDocumento,
                NumeroDocumento = dto.NumeroDocumento,
                UserName = dto.Username,
                Email = dto.Email,
                Password = _passwordHasher.HashPassword(dto.Password),
                PrimerNombre = dto.PrimerNombre,
                SegundoNombre = dto.SegundoNombre,
                PrimerApellido = dto.PrimerApellido,
                SegundoApellido = dto.SegundoApellido,
                IdRol = dto.IdRol
            };

            // Lógica de perfiles
            if (dto.IdRol == 2)
            {
                nuevoUsuario.Profesor = new Profesor();
            }
            else if (dto.IdRol == 3)
            {
                nuevoUsuario.Estudiante = new Estudiante();
            }
            await _unitOfWork.Usuarios.AddAsync(nuevoUsuario);
            await _unitOfWork.SaveChangesAsync();

            // Retornar el DTO de respuesta
            return new UsuarioResponseDto
            {
                IdUsuario = nuevoUsuario.IdUsuario,
                CodUsuario = nuevoUsuario.CodUsuario.ToString(),
                UserName = nuevoUsuario.UserName,
                Email = nuevoUsuario.Email,
                TipoDocumento = nuevoUsuario.TipoDocumento,
                NumeroDocumento = nuevoUsuario.NumeroDocumento,
                NombreCompleto = nuevoUsuario.ObtenerNombreCompleto(),
                Rol = nuevoUsuario.IdRol == 1 ? "Admin" : (nuevoUsuario.IdRol == 2 ? "Profesor" : "Estudiante"),
                Estado = nuevoUsuario.Estado,
                FechaRegistro = nuevoUsuario.FechaRegistro
            };
        }
    }
}