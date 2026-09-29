using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.Interfaces.Persistence
{
    public interface IProfesorRepository : IGenericRepository<Profesor>
    {
        Task<Profesor?> GetByIdUsuarioAsync(int idUsuario);
        Task<IReadOnlyList<Profesor>> GetSolicitudesPendientesAsync();
        Task<Profesor?> GetProfesorConMateriasAsync(int idProfesor);
    }
}
