
namespace SistemaCreditos.Application.DTOs.Usuarios
{
    public class RegistrarUsuarioRequestDto
    {
        public string TipoDocumento { get; set; } = "CC";
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PrimerNombre { get; set; } = string.Empty;
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; } = string.Empty;
        public string? SegundoApellido { get; set; }
        public int IdRol { get; set; }
    }
}
