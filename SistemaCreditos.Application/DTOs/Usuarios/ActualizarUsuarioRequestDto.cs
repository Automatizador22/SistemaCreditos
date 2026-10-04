
namespace SistemaCreditos.Application.DTOs.Usuarios
{
    public class ActualizarUsuarioRequestDto
    {
        public String TipoDocumento { get; set; } = "CC";
        public String NumeroDocumento { get; set; } = String.Empty;
        public String Email { get; set; } = String.Empty;
        public String PrimerNombre { get; set; } = String.Empty;
        public String? SegundoNombre { get; set; }
        public String PrimerApellido { get; set; } = String.Empty;
        public String? SegundoApellido { get; set; }
    }
}
