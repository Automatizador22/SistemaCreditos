namespace SistemaCreditos.Domain.Entities
{
    public class Estudiante
    {
        private readonly List<Inscripcion> _inscripciones = new();
        public int IdEstudiante { get; set; }
        public int IdUsuario { get; set; }


        public Usuario Usuario { get; set; } = null!;

        public IReadOnlyCollection<Inscripcion> Inscripciones => _inscripciones.AsReadOnly();

        public void AgregarInscripcion(Materia materia)
        {
            if (materia == null)
                throw new ArgumentNullException(nameof(materia));

            if (Usuario != null && Usuario.FechaDesvinculacion.HasValue)
                throw new InvalidOperationException("Un estudiante desvinculado no puede inscribir materias.");

            if (_inscripciones.Any(i => i.IdMateria == materia.IdMateria))
                throw new InvalidOperationException("El estudiante ya está inscrito en esta materia.");

            _inscripciones.Add(new Inscripcion
            {
                IdEstudiante = this.IdEstudiante,
                IdMateria = materia.IdMateria,
                FechaInscripcion = DateTime.UtcNow
            });
        }
    }
}
