

namespace SistemaCreditos.Application.DTOs.Usuarios
{
    public class ActualizarDatosUsuarioRequestDto
    {
        public String PrimerNombre { get; set; } = String.Empty;
        public String? SegundoNombre { get; set; }
        public String PrimerApellido { get; set; } = String.Empty;
        public String? SegundoApellido { get; set; }
    }
}
