using Microsoft.EntityFrameworkCore;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Domain.Entities;
using SistemaCreditos.Infrastructure.Persistence.Context;
using SistemaCreditos.Domain.Enums;

namespace SistemaCreditos.Infrastructure.Persistence.Repositories
{
    public class ProfesorRepository : GenericRepository<Profesor>, IProfesorRepository
    {
        public ProfesorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Profesor?> GetByIdUsuarioAsync(int idUsuario)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.IdUsuario == idUsuario);
        }
        public async Task<IReadOnlyList<Profesor>> GetSolicitudesPendientesAsync()
        {
            return await _dbSet.Where(p => p.EstadoRegistro == EstadoRegistroProfesor.PENDIENTE).ToListAsync();
        }
        public async Task<Profesor?> GetProfesorConMateriasAsync(int idProfesor)
        {
            return await _dbSet.Include(p => p.Materias)
                               .FirstOrDefaultAsync(p => p.IdProfesor == idProfesor);
        }
    }
}
