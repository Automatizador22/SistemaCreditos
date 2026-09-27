using SistemaCreditos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditos.Domain.Entities
{
    public class Profesor
    {
        private readonly List<Materia> _materias = new List<Materia>();
        public int IdProfesor { get; set; }
        public int IdUsuario { get; set; }
        public EstadoRegistroProfesor EstadoRegistro { get; set; } = EstadoRegistroProfesor.PENDIENTE;


        //Mejorar para que no use el ICollection
        //public ICollection<Materia> Materias { get; set; } = new List<Materia>();

        public Usuario Usuario { get; set; } = null!;

        public IReadOnlyCollection<Materia> Materias => _materias.AsReadOnly();

        public void AgregarMateria(Materia materia)
        {
            if (materia == null)
                throw new ArgumentNullException(nameof(materia));
            if (EstadoRegistro != EstadoRegistroProfesor.APROBADO)
                throw new InvalidOperationException("El profesor no está aprobado para agregar materias.");
            if (_materias.Any(m => m.IdMateria == materia.IdMateria))
                throw new InvalidOperationException("La materia ya está asociada al profesor.");

            _materias.Add(materia);
        }
    }
}
