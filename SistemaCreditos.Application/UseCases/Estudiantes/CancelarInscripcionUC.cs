

using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;

namespace SistemaCreditos.Application.UseCases.Estudiantes
{
    public class CancelarInscripcionUC
    {
        private readonly IUnitOfWork _unitOfWork;
        private const int DIAS_MAXIMOS_CANCELACION = 5;

        public CancelarInscripcionUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> EjecutarAsync(int IdUsuario, int IdMateria)
        {
            var estudiante = await _unitOfWork.Estudiantes.GetByIdUsuarioAsync(IdUsuario);
            if (estudiante == null)
            {
                throw new ValidationException("Estudiante no encontrado");
            }
            var inscripcion = await _unitOfWork.Inscripciones.GetInscripcionEspecificaAsync(estudiante.IdEstudiante, IdMateria);
            if (inscripcion == null)
            {
                throw new ValidationException("No te encuentras inscrito en esta materia.");
            }
            var tiempoTranscurrido = DateTime.UtcNow - inscripcion.FechaInscripcion;

            if (tiempoTranscurrido.TotalDays > DIAS_MAXIMOS_CANCELACION)
            {
                throw new ValidationException($"No puedes cancelar la materia. Ha superado el límite de {DIAS_MAXIMOS_CANCELACION} días desde la inscripción.");
            }
            _unitOfWork.Inscripciones.Delete(inscripcion);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
