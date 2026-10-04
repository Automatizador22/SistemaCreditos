
namespace SistemaCreditos.Application.DTOs.Materias
{
    public class CrearMateriaRequestDto
    {
        public string NombreMateria { get; set; } = string.Empty;
        public int Creditos { get; set; } = 3;
        public int? IdProfesor { get; set; }
    }
}
