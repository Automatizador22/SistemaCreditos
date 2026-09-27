using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCreditos.Domain.Entities
{
    public class Rol
    {
        private readonly List<Usuario> _usuarios = new();

        public int IdRol { get; set; }
        public String Nombre { get; set; } = String.Empty;
        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        //Mejorar para que no use el ICollection
        //public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public IReadOnlyCollection<Usuario> Usuarios => _usuarios.AsReadOnly();

        public void AgregarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));
            if (_usuarios.Any(u => u.IdUsuario == usuario.IdUsuario))
                throw new InvalidOperationException("El usuario ya está asociado al rol.");

            _usuarios.Add(usuario);
        }
    }
}
