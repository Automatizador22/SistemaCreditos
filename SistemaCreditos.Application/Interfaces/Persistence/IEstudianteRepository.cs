using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.Interfaces.Persistence
{
    public interface IEstudianteRepository : IGenericRepository<Estudiante>
    {
        Task<Estudiante?> GetByIdUsuarioAsync(int idUsuario);
        Task<Estudiante?> GetEstudianteConInscripcionesAsync(int idEstudiante);
    }
}
