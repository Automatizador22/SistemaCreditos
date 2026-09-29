using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditos.Application.DTOs.Materias
{
    public class CrearMateriaDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int Creditos { get; set; } = 3;
        public int? IdProfesor { get; set; }
    }
}
