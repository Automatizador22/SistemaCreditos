using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditos.Application.DTOs.Profesores
{
    public class AprobarRechazarSolicitudDto
    {
        public int IdProfesor { get; set; }
        public bool Aprobar { get; set; }
    }
}
