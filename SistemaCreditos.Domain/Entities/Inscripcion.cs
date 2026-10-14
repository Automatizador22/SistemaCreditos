
namespace SistemaCreditos.Domain.Entities
{
    public class Inscripcion
    {
        public int IdInscripcion { get; set; }
        public int IdEstudiante { get; set; }
        public int IdMateria { get; set; }
        public DateTime FechaInscripcion { get; set; } = DateTime.UtcNow;

        public Estudiante Estudiante { get; set; } = null!;
        public Materia Materia { get; set; } = null!;
    }
}
