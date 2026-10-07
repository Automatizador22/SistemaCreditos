
namespace SistemaCreditos.Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuarios { get; }
        IProfesorRepository Profesores { get; }
        IEstudianteRepository Estudiantes { get; }
        IMateriaRepository Materias { get; }
        IInscripcionRepository Inscripciones { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
