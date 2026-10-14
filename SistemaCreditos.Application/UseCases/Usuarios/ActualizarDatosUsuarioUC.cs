using SistemaCreditos.Application.DTOs.Usuarios;
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;


namespace SistemaCreditos.Application.UseCases.Usuarios
{
    public class ActualizarDatosUsuarioUC
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarDatosUsuarioUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> EjecutarAsync(int idUsuario, ActualizarDatosUsuarioRequestDto dto)
        {
            var usuario = await _unitOfWork.Usuarios.GetByIdAsync(idUsuario);

            if (usuario == null)
            {
                throw new ValidationException("Usuario no encontrado.");
            }
            usuario.ActualizarDatosBasicos(
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
