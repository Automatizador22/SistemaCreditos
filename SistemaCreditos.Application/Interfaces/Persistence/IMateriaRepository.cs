using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.Interfaces.Persistence
{
    public interface IMateriaRepository : IGenericRepository<Materia>
    {
        Task<IReadOnlyList<Materia>> GetMateriasDisponiblesAsync();
        Task<IReadOnlyList<Materia>> GetMateriasConProfesorAsync();
        Task<IReadOnlyList<Materia>> GetMateriasPorProfesorAsync(int idProfesor);
        Task<IReadOnlyList<Materia>> GetMateriasPorEstudianteAsync(int idEstudiante);

        //Task<int> GetTotalCreditosInscritosAsync(int idEstudiante);

    }
}
