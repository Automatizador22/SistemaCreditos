
namespace SistemaCreditos.Application.DTOs.Estudiantes
{
    public class EstudianteResponseDto
    {
        public int IdEstudiante { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
