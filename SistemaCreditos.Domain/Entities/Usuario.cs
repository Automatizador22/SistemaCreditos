using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditos.Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public Guid CodUsuario { get; set; } = Guid.NewGuid();
        public String NumeroDocumento { get; set; } = String.Empty;
        public String TipoDocumento { get; set; } = "CC";
        public String UserName { get; set; } = String.Empty;
        public String Email { get; set; } = String.Empty;
        public String Password { get; set; } = String.Empty;
        public String PrimerNombre { get; set; } = String.Empty;
        public String? SegundoNombre { get; set; }
        public String PrimerApellido { get; set; } = String.Empty;
        public String? SegundoApellido { get; set; }
        public bool Estado { get; private set; } = true;
        public int IdRol { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime? FechaActualizacion { get; private set; }
        public DateTime? FechaDesvinculacion { get; private set; }

        public Rol Rol { get; set; } = null!;
        public Profesor? Profesor { get; set; }
        public Estudiante? Estudiante { get; set; }

        public string ObtenerNombreCompleto()
        {
            return string.Join(" ", new[] { PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido }
                .Where(s => !string.IsNullOrWhiteSpace(s)));
        }
        public void DesactivarCuenta()
        {
            Estado = false;
            FechaActualizacion = DateTime.Now;
        }
        public void ActivarCuenta()
        {
            Estado = true;
            FechaActualizacion = DateTime.Now;
        }
        public void Desvincular()
        {
            Estado = false;
            FechaDesvinculacion = DateTime.Now;
            FechaActualizacion = DateTime.Now;
        }
    }
}
