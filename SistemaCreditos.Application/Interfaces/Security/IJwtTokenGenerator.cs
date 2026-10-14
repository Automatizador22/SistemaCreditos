
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerarToken(Usuario usuario);
    }
}
