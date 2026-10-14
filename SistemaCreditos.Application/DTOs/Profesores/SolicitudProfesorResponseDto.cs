
namespace SistemaCreditos.Application.DTOs.Profesores
{
    public class SolicitudProfesorResponseDto
    {
        public int IdProfesor { get; set; }
        public string NombreProfesor { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EstadoRegistro { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
