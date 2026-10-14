using SistemaCreditos.Application.DTOs.Materias;

namespace SistemaCreditos.Application.DTOs.Estudiantes
{
    public class EstudianteResumenDto
    {
        public int IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; } = string.Empty;
        public int TotalMateriasInscritas { get; set; }
        public int MateriasMaximasPermitidas { get; set; } = 3;
        public List<MateriaDetalleResponseDto> MateriasInscritas { get; set; } = new();
    }
}
