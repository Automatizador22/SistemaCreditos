using SistemaCreditos.Application.DTOs.Estudiantes;
using SistemaCreditos.Application.DTOs.Materias;
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.UseCases.Materias
{
    public class ObtenerMateriasDisponiblesUC
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObtenerMateriasDisponiblesUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RegistrarMateriaResponseDto>> EjecutarAsync()
        {
            var materias = await _unitOfWork.Materias.GetMateriasDisponiblesAsync();

            var materiasDisponibles = materias.Select(m => new RegistrarMateriaResponseDto
            {
                IdMateria = m.IdMateria,
                NombreMateria = m.NombreMateria,
                Creditos = m.Creditos,
                NombreProfesor = m.Profesor != null && m.Profesor.Usuario != null
                    ? m.Profesor.Usuario.ObtenerNombreCompleto()
                    : "Sin profesor asignado",
                FechaRegistro = m.FechaRegistro,
                TotalEstudiantesInscritos = m.Inscripciones != null ? m.Inscripciones.Count : 0
    }).ToList();

            return materiasDisponibles;

        }


    }
}
