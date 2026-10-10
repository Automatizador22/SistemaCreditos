
namespace SistemaCreditos.Application.DTOs.Materias
{
    public class MateriaDetalleResponseDto
    {
        public int IdMateria { get; set; }
        public string CodMateria { get; set; } = string.Empty;
        public string NombreMateria { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public string? NombreProfesor { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int TotalEstudiantesInscritos { get; set; }
    }
}
