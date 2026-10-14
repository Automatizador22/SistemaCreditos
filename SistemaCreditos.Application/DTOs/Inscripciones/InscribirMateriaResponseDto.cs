namespace SistemaCreditos.Application.DTOs.Inscripciones
{
    public class InscribirMateriaResponseDto
    {
        public int IdInscripcion { get; set; }
        public int IdMateria { get; set; }
        public String NombreMateria { get; set; } = String.Empty;
        public int Creditos { get; set; }
        public String? NombreProfesor { get; set; }
        public DateTime FechaInscripcion { get; set; }
    }
}
