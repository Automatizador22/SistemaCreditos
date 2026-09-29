
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.Interfaces.Persistence
{
    public interface IProfesorRepository : IGenericRepository<Profesor>
    {
        Task<Profesor?> GetByIdUsuarioAsync(int idUsuario);
        Task<IReadOnlyList<Profesor>> GetSolicitudesPendientesAsync();
        Task<Profesor?> GetProfesorConMateriasAsync(int idProfesor);
    }
}
