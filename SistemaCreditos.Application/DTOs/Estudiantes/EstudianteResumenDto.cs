using SistemaCreditos.Application.DTOs.Materias;

namespace SistemaCreditos.Application.DTOs.Estudiantes
{
    public class EstudianteResumenCreditosDto
    {
        public int IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; } = string.Empty;
        public int TotalCreditosInscritos { get; set; }
        public int CreditosMaximosPermitidos { get; set; } = 3;
        public List<RegistrarMateriaResponseDto> MateriasInscritas { get; set; } = new();
    }
}
