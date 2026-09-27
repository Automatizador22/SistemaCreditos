using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditos.Domain.Entities
{
    public class Materia
    {
        private readonly List<Inscripcion> _inscripciones = new();
        public int IdMateria { get; set; }
        public Guid CodMateria { get; set; } = Guid.NewGuid();
        public String NombreMateria { get; set; } = String.Empty;
        public int Creditos { get; set; } = 3;
        public int? IdProfesor { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime? FechaActualizacion { get; set; }

        public Profesor? Profesor { get; set; }
        //Mejorar para que no use el ICollection
        //public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();

        public IReadOnlyCollection<Inscripcion> Inscripciones => _inscripciones.AsReadOnly();

    }
}
