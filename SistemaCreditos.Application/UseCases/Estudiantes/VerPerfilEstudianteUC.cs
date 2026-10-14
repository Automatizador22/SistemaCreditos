using SistemaCreditos.Application.DTOs.Estudiantes;
using SistemaCreditos.Application.DTOs.Materias;
using SistemaCreditos.Application.Exceptions;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.UseCases.Estudiantes
{
    public class VerPerfilEstudianteUC
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerPerfilEstudianteUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EstudianteResumenDto> EjecutarAsync(int idUsuario)
        {
            var usuario = await _unitOfWork.Usuarios.GetByIdAsync(idUsuario);
            var estudiante = await _unitOfWork.Estudiantes.GetByIdUsuarioAsync(idUsuario);

            if (usuario == null || estudiante == null)
            {
                throw new ValidationException("Perfil de estudiante no encontrado.");
            }
            var misMaterias = await _unitOfWork.Materias.GetMateriasPorEstudianteAsync(estudiante.IdEstudiante);

            var perfil = new EstudianteResumenDto
            {
                IdEstudiante = estudiante.IdEstudiante,
                NombreEstudiante = estudiante.Usuario != null
                    ? estudiante.Usuario.ObtenerNombreCompleto()
                    : "Nombre no disponible",
                TotalMateriasInscritas = misMaterias.Count,
                MateriasMaximasPermitidas = 3,

                MateriasInscritas = misMaterias.Select(m => new MateriaDetalleResponseDto
                {
                    IdMateria = m.IdMateria,
                    CodMateria = m.CodMateria.ToString(),
                    NombreMateria = m.NombreMateria,
                    Creditos = m.Creditos,
                    NombreProfesor = m.Profesor != null && m.Profesor.Usuario != null
                    ? m.Profesor.Usuario.ObtenerNombreCompleto()
                    : "Sin profesor asignado",
                    FechaRegistro = m.FechaRegistro,
                    TotalEstudiantesInscritos = m.Inscripciones != null ? m.Inscripciones.Count : 0
                }).ToList()
            };

            return perfil;
        }
    }
}