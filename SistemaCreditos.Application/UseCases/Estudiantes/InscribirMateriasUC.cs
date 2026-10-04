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

            var creditosActuales = await _unitOfWork.Materias.GetTotalCreditosInscritosAsync(estudiante.IdEstudiante);
            if (creditosActuales + materia.Creditos > 3)
            {
                throw new ValidationException($"El estudiante con ID {dto.IdUsuarioEstudiante} no puede inscribirse a la materia {materia.NombreMateria} porque excedería el límite de créditos.");
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
