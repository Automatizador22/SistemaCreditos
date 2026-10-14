
namespace SistemaCreditos.Application.DTOs.Usuarios
{
    public class UsuarioResponseDto
    {
        public int IdUsuario { get; set; }
        public String CodUsuario { get; set; } = String.Empty;
        public String UserName { get; set; } = String.Empty;
        public String Email { get; set; } = String.Empty;
        public String TipoDocumento { get; set; } = String.Empty;
        public String NumeroDocumento { get; set; } = String.Empty;
        public String NombreCompleto { get; set; } = String.Empty;
        public String Rol { get; set; } = String.Empty;
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
