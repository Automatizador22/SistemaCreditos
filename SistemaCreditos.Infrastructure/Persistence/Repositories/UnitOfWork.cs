using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Infrastructure.Persistence.Context;

namespace SistemaCreditos.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUsuarioRepository Usuarios { get; }
        public IProfesorRepository Profesores { get; private set; }
        public IEstudianteRepository Estudiantes { get; private set; }
        public IMateriaRepository Materias { get; private set; }
        public IInscripcionRepository Inscripciones { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Usuarios = new UsuarioRepository(_context);
            Profesores = new ProfesorRepository(_context);
            Estudiantes = new EstudianteRepository(_context);
            Materias = new MateriaRepository(_context);
            Inscripciones = new InscripcionRepository(_context);
        }

        //Enviar cambios a la base de datos
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        // Liberar conexion a la base de datos
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
