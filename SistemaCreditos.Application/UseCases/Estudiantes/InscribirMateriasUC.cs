using SistemaCreditos.Application.DTOs.Inscripciones;
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;

namespace SistemaCreditos.Application.UseCases.Estudiantes
{
    public class InscribirMateriasUC
    {
        private readonly IUnitOfWork _unitOfWork;
        public InscribirMateriasUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task <InscribirMateriaResponseDto> EjecutarAsync(InscribirMateriaRequestDto dto)
        {
            var estudiante = await _unitOfWork.Estudiantes.GetByIdUsuarioAsync(dto.IdUsuarioEstudiante);
            if (estudiante == null)
            {
                throw new NotFoundException($"El estudiante con ID {dto.IdUsuarioEstudiante} no existe.");
            }
            var materia = await _unitOfWork.Materias.GetByIdAsync(dto.IdMateria);
            if (materia == null)
            {
                throw new NotFoundException($"La materia con ID {dto.IdMateria} no existe.");
            }
            var materiasDelEstudiante = await _unitOfWork.Materias.GetMateriasPorEstudianteAsync(dto.IdUsuarioEstudiante);

            if (materiasDelEstudiante.Count >= 3)
            {
                throw new ValidationException("Has alcanzado el límite máximo. Solo puedes seleccionar 3 materias.");
            }
            if (materiasDelEstudiante.Any(m => m.IdMateria == dto.IdMateria))
            {
                throw new ValidationException("Ya te encuentras inscrito en esta materia.");
            }
            if (materia.IdProfesor.HasValue)
            {
                var tieneMismoProfesor = materiasDelEstudiante.Any(m => m.IdProfesor == materia.IdProfesor.Value);
                if (tieneMismoProfesor)
                {
                    throw new ValidationException("No puedes inscribir esta materia. Ya tienes otra clase asignada con este mismo profesor.");
                }
            }
            estudiante.AgregarInscripcion(materia);
            await _unitOfWork.SaveChangesAsync();

            return new InscribirMateriaResponseDto
            {
                IdInscripcion = estudiante.Inscripciones.Last().IdInscripcion,
                IdMateria = materia.IdMateria,
                NombreMateria = materia.NombreMateria,
                Creditos = materia.Creditos,
                NombreProfesor = materia.Profesor?.Usuario?.ObtenerNombreCompleto(),
                FechaInscripcion = DateTime.UtcNow
            };
        }
    }
}
