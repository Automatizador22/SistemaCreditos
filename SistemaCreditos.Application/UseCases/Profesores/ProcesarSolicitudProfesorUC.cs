using SistemaCreditos.Application.DTOs.Profesores;
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Application.Interfaces.ExternalServices;
using SistemaCreditos.Domain.Enums;

namespace SistemaCreditos.Application.UseCases.Profesores
{
    public class ProcesarSolicitudProfesorUC
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public ProcesarSolicitudProfesorUC(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<bool> EjecutarAsync(ProcesarSolicitudRequestDto dto)
        {
            var profesor = await _unitOfWork.Profesores.GetByIdAsync(dto.IdProfesor);
            if (profesor == null) throw new ValidationException("El perfil de profesor no existe.");

            var usuario = await _unitOfWork.Usuarios.GetByIdAsync(profesor.IdUsuario);
            if (usuario == null) throw new ValidationException("Usuario no encontrado.");

            if (profesor.EstadoRegistro != EstadoRegistroProfesor.PENDIENTE)
            {
                throw new ValidationException($"No se puede procesar. El profesor actualmente está en estado: {profesor.EstadoRegistro}");
            }
            if (dto.Aprobar)
            {
                profesor.AprobarSolicitud();
            }
            else
            {
                profesor.RechazarSolicitud();
            }
            _unitOfWork.Profesores.Update(profesor);
            await _unitOfWork.SaveChangesAsync();

            string decision = dto.Aprobar ? "APROBADA" : "RECHAZADA";
            string nombreCompleto = usuario.ObtenerNombreCompleto();

            string asunto = $"Tu solicitud ha sido - {decision}";
            string cuerpo = dto.Aprobar
                ? $"Hola {nombreCompleto}, tu solicitud ha sido aprobada. ¡Bienvenido!"
                : $"Hola {nombreCompleto}, tu solicitud fue rechazada. Podrás intentarlo de nuevo en 24 horas.";


            _ = _emailService.EnviarEmailAsync(usuario.Email, asunto, cuerpo);

            return true;
        }
    }
}