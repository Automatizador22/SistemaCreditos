
namespace SistemaCreditos.Application.DTOs.Materias
{
    public class RegistrarMateriaRequestDto
    {
        public string NombreMateria { get; set; } = string.Empty;
        public int? IdProfesor { get; set; }
    }
}
