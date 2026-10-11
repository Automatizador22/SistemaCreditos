using SistemaCreditos.Application.DTOs.Estudiantes;
using SistemaCreditos.Application.Interfaces.Persistence;

namespace SistemaCreditos.Application.UseCases.Estudiantes
{
    public class ObtenerTodosLosEstudiantesUC
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObtenerTodosLosEstudiantesUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<EstudianteResponseDto>> EjecutarAsync()
        {
            var estudiantes = await _unitOfWork.Estudiantes.ObtenerEstudiantesDetalladosAsync();

            var listaEstudiantes = estudiantes.Select(e => new EstudianteResponseDto
            {
                IdEstudiante = e.IdEstudiante,
                NombreCompleto = e.Usuario != null ? e.Usuario.ObtenerNombreCompleto() : "Sin nombre registrado",
                Email = e.Usuario != null ? e.Usuario.Email : "Sin correo registrado"
            }).ToList();

            return listaEstudiantes;
        }
    }
}