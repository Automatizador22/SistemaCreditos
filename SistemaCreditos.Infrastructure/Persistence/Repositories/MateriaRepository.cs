using Microsoft.EntityFrameworkCore;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Domain.Entities;
using SistemaCreditos.Infrastructure.Persistence.Context;

namespace SistemaCreditos.Infrastructure.Persistence.Repositories
{
    public class MateriaRepository : GenericRepository<Materia>, IMateriaRepository
    {
        public MateriaRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IReadOnlyList<Materia>> GetMateriasDisponiblesAsync()
        {
            return await _dbSet
                .Include(m => m.Profesor)
                .ThenInclude(p => p.Usuario)
                .Include(m => m.Inscripciones)
                .ToListAsync();
        }
        public async Task<IReadOnlyList<Materia>> GetMateriasConProfesorAsync()
        {
            return await _dbSet.Include(m => m.Profesor).ToListAsync();
        }
        public async Task<IReadOnlyList<Materia>> GetMateriasPorProfesorAsync(int idProfesor)
        {
            return await _dbSet.Where(m => m.IdProfesor == idProfesor).ToListAsync();
        }
        public async Task<IReadOnlyList<Materia>> GetMateriasPorEstudianteAsync(int idEstudiante)
        {
            return await _dbSet
                .Include(m => m.Inscripciones)
                .Include(m => m.Profesor)
                .ThenInclude(p => p.Usuario)
                .Where(m => m.Inscripciones.Any(i => i.IdEstudiante == idEstudiante))
                .ToListAsync();
        }
        public async Task<int> GetTotalCreditosInscritosAsync(int idEstudiante)
        {
            return await _dbSet
                .Where(m => m.Inscripciones.Any(i => i.IdEstudiante == idEstudiante))
                .SumAsync(m => m.Creditos);
        }
    }
}
