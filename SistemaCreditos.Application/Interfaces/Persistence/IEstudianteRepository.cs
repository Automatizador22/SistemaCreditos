using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Application.Interfaces.Persistence
{
    public interface IEstudianteRepository : IGenericRepository<Estudiante>
    {
        Task<Estudiante?> GetByIdUsuarioAsync(int idUsuario);
        Task<Estudiante?> GetEstudianteConInscripcionesAsync(int idEstudiante);
    }
}
