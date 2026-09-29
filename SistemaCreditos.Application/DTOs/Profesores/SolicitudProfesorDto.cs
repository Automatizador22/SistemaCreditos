using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditos.Application.DTOs.Profesores
{
    public class SolicitudProfesorDto
    {
        public int IdProfesor { get; set; }
        public int IdUsuario { get; set; }
        public string NombreProfesor { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string EstadoRegistro { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
