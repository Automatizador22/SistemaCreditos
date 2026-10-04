using SistemaCreditos.Application.DTOs.Usuarios;
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Application.Interfaces.Security;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.UseCases.Usuarios
{
    public class RegistrarUsuarioUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        // 1. Inyección de Dependencias: Pedimos los enchufes que necesitamos
        public RegistrarUsuarioUseCase(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        // 2. El método principal que ejecuta la acción
        public async Task<UsuarioResponseDto> EjecutarAsync(RegistrarUsuarioRequestDto dto)
        {
            // REGLA 1: Verificar que el email no exista
            if (await _unitOfWork.Usuarios.ExistsEmailAsync(dto.Email))
            {
                throw new ValidationException($"El correo '{dto.Email}' ya está registrado.");
            }

            // REGLA 2: Verificar que el username no exista
            if (await _unitOfWork.Usuarios.ExistsUsernameAsync(dto.Username))
            {
                throw new ValidationException($"El usuario '{dto.Username}' ya está en uso.");
            }

            // 3. Crear la entidad principal (El núcleo de tu dominio)
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

            // 4. Lógica de perfiles automáticos: 
            // Si el rol es Profesor (ej. ID 2), le creamos su perfil de profesor atado.
            if (dto.IdRol == 2)
            {
                nuevoUsuario.Profesor = new Profesor();
            }
            // Si el rol es Estudiante (ej. ID 3), le creamos su perfil de estudiante atado.
            else if (dto.IdRol == 3)
            {
                nuevoUsuario.Estudiante = new Estudiante();
            }

            // 5. Agregar a la memoria y guardar en la Base de Datos (Transacción)
            await _unitOfWork.Usuarios.AddAsync(nuevoUsuario);
            await _unitOfWork.SaveChangesAsync();

            // 6. Retornar el DTO de respuesta (ocultando datos sensibles)
            return new UsuarioResponseDto
            {
                IdUsuario = nuevoUsuario.IdUsuario,
                CodUsuario = nuevoUsuario.CodUsuario.ToString(),
                UserName = nuevoUsuario.UserName,
                Email = nuevoUsuario.Email,
                TipoDocumento = nuevoUsuario.TipoDocumento,
                NumeroDocumento = nuevoUsuario.NumeroDocumento,
                NombreCompleto = nuevoUsuario.ObtenerNombreCompleto(),
                // Como no cargamos el Rol de la BD, enviamos un texto genérico o vacío por ahora
                Rol = nuevoUsuario.IdRol == 1 ? "Admin" : (nuevoUsuario.IdRol == 2 ? "Profesor" : "Estudiante"),
                Estado = nuevoUsuario.Estado,
                FechaRegistro = nuevoUsuario.FechaRegistro
            };
        }
    }
}