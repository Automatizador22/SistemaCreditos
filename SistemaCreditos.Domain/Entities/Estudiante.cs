using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditos.Domain.Entities
{
    public class Estudiante
    {
        private readonly List<Inscripcion> _inscripciones = new();
        public int IdEstudiante { get; set; }
        public int IdUsuario { get; set; }


        public Usuario Usuario { get; set; } = null!;
        //Mejorar para que no use el ICollection
        //public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();

        public IReadOnlyCollection<Inscripcion> Inscripciones => _inscripciones.AsReadOnly();

        public void AgregarInscripcion(Materia materia)
        {
            if (materia == null)
                throw new ArgumentNullException(nameof(materia));

            // Validar si el usuario asociado está desvinculado
            if (Usuario != null && Usuario.FechaDesvinculacion.HasValue)
                throw new InvalidOperationException("Un estudiante desvinculado no puede inscribir materias.");

            // Validar duplicado por IdMateria (incluso si IdInscripcion aún es 0)
            if (_inscripciones.Any(i => i.IdMateria == materia.IdMateria))
                throw new InvalidOperationException("El estudiante ya está inscrito en esta materia.");

            _inscripciones.Add(new Inscripcion
            {
                IdEstudiante = this.IdEstudiante,
                IdMateria = materia.IdMateria,
                FechaInscripcion = DateTime.Now
            });
        }
    }
}
