using Microsoft.EntityFrameworkCore;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Domain.Entities;
using SistemaCreditos.Infrastructure.Persistence.Context;

namespace SistemaCreditos.Infrastructure.Persistence.Repositories
{
    public class EstudianteRepository : GenericRepository<Estudiante>, IEstudianteRepository
    {
        public EstudianteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Estudiante?> GetByIdUsuarioAsync(int idUsuario)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario);
        }
        public async Task<Estudiante?> GetEstudianteConInscripcionesAsync(int idEstudiante)
        {
            return await _dbSet.Include(e => e.Inscripciones)
                               .FirstOrDefaultAsync(e => e.IdEstudiante == idEstudiante);
        }
    }
}
