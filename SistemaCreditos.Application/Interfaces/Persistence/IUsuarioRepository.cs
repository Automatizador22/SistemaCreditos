using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.Interfaces.Persistence
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> GetByUsernameOrEmailAsync(string identifier);
        Task<Usuario?> GetByDocumentoAsync(string numeroDocumento);
        Task<bool> ExistsUsernameAsync(string username);
        Task<bool> ExistsEmailAsync(string email);
    }
}
