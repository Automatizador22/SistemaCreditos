using SistemaCreditos.Application.DTOs.Estudiantes; 
using SistemaCreditos.Application.Interfaces.Persistence;

namespace SistemaCreditos.Application.UseCases.Estudiantes
{
    public class VerCompañerosClaseUC
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerCompañerosClaseUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<MateriaCompañerosDto>> EjecutarAsync(int idUsuario)
        {
            var estudianteActual = await _unitOfWork.Estudiantes.GetByIdUsuarioAsync(idUsuario);
            if (estudianteActual == null) throw new Exception("Perfil de estudiante no encontrado.");

            var misMaterias = await _unitOfWork.Materias.GetMateriasPorEstudianteAsync(estudianteActual.IdEstudiante);
            var reporte = new List<MateriaCompañerosDto>();

            foreach (var materia in misMaterias)
            {
                var inscripcionesMateria = await _unitOfWork.Inscripciones.GetInscripcionesPorMateriaAsync(materia.IdMateria);

                string nombreProfesorReal = "Sin profesor asignado";

                if (materia.Profesor != null && materia.Profesor.Usuario != null)
                {
                    nombreProfesorReal = materia.Profesor.Usuario.ObtenerNombreCompleto();
                }

                var dto = new MateriaCompañerosDto
                {
                    NombreMateria = materia.NombreMateria,
                    NombreProfesor = nombreProfesorReal
                };

                foreach (var inscripcion in inscripcionesMateria)
                {
                    if (inscripcion.IdEstudiante != estudianteActual.IdEstudiante)
                    {
                        string nombreCompañero = inscripcion.Estudiante.Usuario.ObtenerNombreCompleto();
                        dto.Compañeros.Add(nombreCompañero);
                    }
                }
                reporte.Add(dto);
            }

            return reporte;
        }
    }
}