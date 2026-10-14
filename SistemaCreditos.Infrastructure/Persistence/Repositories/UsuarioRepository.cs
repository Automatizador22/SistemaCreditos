using Microsoft.EntityFrameworkCore;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Domain.Entities;
using SistemaCreditos.Infrastructure.Persistence.Context;

namespace SistemaCreditos.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> GetByUsernameOrEmailAsync(string identifier)
        {
            return await _dbSet
                    .Include(u => u.Rol)
                    .FirstOrDefaultAsync(u => u.UserName == identifier || u.Email == identifier);
        }

        public async Task<Usuario?> GetByDocumentoAsync(string numeroDocumento)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.NumeroDocumento == numeroDocumento);
        }

        public async Task<bool> ExistsUsernameAsync(string username)
        {
            return await _dbSet.AnyAsync(u => u.UserName == username);
        }

        public async Task<bool> ExistsEmailAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }
    }
}

