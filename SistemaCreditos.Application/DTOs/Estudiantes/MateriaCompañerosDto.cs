

namespace SistemaCreditos.Application.DTOs.Estudiantes
{
    public class MateriaCompañerosDto
    {
        public string NombreMateria { get; set; } = string.Empty;
        public string NombreProfesor { get; set; } = string.Empty;
        public List<string> Compañeros { get; set; } = new List<string>();
    }
}
