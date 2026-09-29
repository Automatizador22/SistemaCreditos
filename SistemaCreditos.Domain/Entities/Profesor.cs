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
        private readonly List<Materia> _materias = new();
        public int IdProfesor { get; set; }
        public int IdUsuario { get; set; }
        public EstadoRegistroProfesor EstadoRegistro { get; private set; } = EstadoRegistroProfesor.PENDIENTE;

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public DateTime? FechaActualizacion { get; private set; }

        public Usuario Usuario { get; set; } = null!;
        public IReadOnlyCollection<Materia> Materias => _materias.AsReadOnly();

        public void AprobarSolicitud()
        {
            EstadoRegistro = EstadoRegistroProfesor.APROBADO;
            FechaActualizacion = DateTime.UtcNow;
        }
        public void RechazarSolicitud()
        {
            EstadoRegistro = EstadoRegistroProfesor.RECHAZADO;
            FechaActualizacion = DateTime.UtcNow;
        }

        public void Repostular()
        {
            if (EstadoRegistro != EstadoRegistroProfesor.RECHAZADO)
                throw new InvalidOperationException("Solo un profesor en estado RECHAZADO puede volver a postularse.");
            if (FechaActualizacion.HasValue)
            {
                var horasTranscurridas = (DateTime.UtcNow - FechaActualizacion.Value).TotalHours;
                if (horasTranscurridas < 24)
                {
                    var horasRestantes = Math.Ceiling(24 - horasTranscurridas);
                    throw new InvalidOperationException($"Tu solicitud fue rechazada recientemente. Debes esperar {horasRestantes} hora(s) más para volver a solicitar la aprobación.");
                }
            }
            EstadoRegistro = EstadoRegistroProfesor.PENDIENTE;
            FechaActualizacion = DateTime.UtcNow;
        }

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
