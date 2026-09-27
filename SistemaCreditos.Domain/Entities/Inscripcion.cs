using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditos.Domain.Entities
{
    public class Inscripcion
    {
        public int IdInscripcion { get; set; }
        public int IdEstudiante { get; set; }
        public int IdMateria { get; set; }
        public DateTime FechaInscripcion { get; set; } = DateTime.Now;

        public Estudiante Estudiante { get; set; } = null!;
        public Materia Materia { get; set; } = null!;
    }
}
