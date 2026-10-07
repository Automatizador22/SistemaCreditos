using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.Interfaces.Persistence
{
    public interface IInscripcionRepository : IGenericRepository<Inscripcion>
    {
        Task<IReadOnlyList<Inscripcion>> GetInscripcionesPorMateriaAsync(int idMateria);
    }
}
