using Microsoft.EntityFrameworkCore;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Domain.Entities;
using SistemaCreditos.Infrastructure.Persistence.Context;

namespace SistemaCreditos.Infrastructure.Persistence.Repositories
{
    public class InscripcionRepository : GenericRepository<Inscripcion>, IInscripcionRepository
    {
        public InscripcionRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IReadOnlyList<Inscripcion>> GetInscripcionesPorMateriaAsync(int idMateria)
        {
            return await _dbSet
                .Include(i => i.Estudiante)
                    .ThenInclude(e => e.Usuario)
                .Where(i => i.IdMateria == idMateria)
                .ToListAsync();
        }
        public async Task<IReadOnlyList<Inscripcion>> GetInscripcionesPorEstudianteAsync(int idEstudiante)
        {
            return await _dbSet
                .Include(i => i.Materia)
                .Where(i => i.IdEstudiante == idEstudiante)
                .ToListAsync();
        }
        public async Task<Inscripcion?> GetInscripcionEspecificaAsync(int idEstudiante, int idMateria)
        {
            return await _dbSet
                .Include(i => i.Materia)
                .Include(i => i.Estudiante)
                    .ThenInclude(e => e.Usuario)
                .FirstOrDefaultAsync(i => i.IdEstudiante == idEstudiante && i.IdMateria == idMateria);
        }
    }
}