using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditos.Application.DTOs.Materias
{
    public class MateriaDetalleDto
    {
        public int IdMateria { get; set; }
        public string CodMateria { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public int? IdProfesor { get; set; }
        public string? NombreProfesor { get; set; }
        public int TotalEstudiantesInscritos { get; set; }
    }
}
