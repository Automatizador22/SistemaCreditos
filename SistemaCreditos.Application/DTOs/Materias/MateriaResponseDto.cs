
namespace SistemaCreditos.Application.DTOs.Materias
{
    public class MateriaResponseDto
    {
        public int IdMateria { get; set; }
        public Guid CodMateria { get; set; } = Guid.Empty;
        public string NombreMateria { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public int? IdProfesor { get; set; }
        public string? NombreProfesor { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int TotalEstudiantesInscritos { get; set; }
    }
}
