using SistemaCreditos.Application.DTOs.Usuarios;
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;

namespace SistemaCreditos.Application.UseCases.Usuarios
{
    public class ActualizarUsuarioAdminUC
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarUsuarioAdminUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> EjecutarAsync(int idUsuario, ActualizarUsuarioRequestDto dto)
        {
            var usuario = await _unitOfWork.Usuarios.GetByIdAsync(idUsuario);
            if (usuario == null)
            {
                throw new ValidationException("El usuario especificado no existe.");
            }
            if (usuario.Email != dto.Email)
            {
                bool existeEmail = await _unitOfWork.Usuarios.ExistsEmailAsync(dto.Email);
                if (existeEmail)
                    throw new ValidationException($"El correo {dto.Email} ya está registrado por otro usuario.");
            }
            if (usuario.NumeroDocumento != dto.NumeroDocumento)
            {
                var usuarioConDoc = await _unitOfWork.Usuarios.GetByDocumentoAsync(dto.NumeroDocumento);
                if (usuarioConDoc != null)
                    throw new ValidationException($"El documento {dto.NumeroDocumento} ya está registrado en el sistema.");
            }
            usuario.ActualizarDatosPorAdmin(
                dto.TipoDocumento,
                dto.NumeroDocumento,
                dto.Email,
                dto.PrimerNombre,
                dto.SegundoNombre,
                dto.PrimerApellido,
                dto.SegundoApellido
            );
            _unitOfWork.Usuarios.Update(usuario);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}