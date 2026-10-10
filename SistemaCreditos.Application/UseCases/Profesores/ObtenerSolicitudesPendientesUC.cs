

using SistemaCreditos.Application.DTOs.Profesores;
using SistemaCreditos.Application.Interfaces.Persistence;

namespace SistemaCreditos.Application.UseCases.Profesores
{
    public class ObtenerSolicitudesPendientesUC
    {
        private readonly IUnitOfWork _unitOfWork;
        public ObtenerSolicitudesPendientesUC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<SolicitudProfesorResponseDto>> EjecutarAsync()
        {
            var solicitudesPendientes = await _unitOfWork.Profesores.GetSolicitudesPendientesAsync();
            return solicitudesPendientes.Select(p => new SolicitudProfesorResponseDto
            {
                IdProfesor = p.IdProfesor,
                NombreProfesor = p.Usuario.ObtenerNombreCompleto(),
                NumeroDocumento = p.Usuario.NumeroDocumento,
                Email = p.Usuario.Email,
                EstadoRegistro = p.EstadoRegistro.ToString(),
                FechaRegistro = p.FechaRegistro,
                FechaActualizacion = p.FechaActualizacion
            }).ToList();
        }
    }
}
