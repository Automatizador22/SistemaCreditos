using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Infrastructure.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");

            builder.HasKey(u => u.IdUsuario);
            builder.Property(u => u.IdUsuario).HasColumnName("id_usuario");

            builder.Property(u => u.CodUsuario).HasColumnName("cod_usuario");
            builder.HasIndex(u => u.CodUsuario).IsUnique();

            builder.Property(u => u.NumeroDocumento).HasColumnName("numero_documento");
            builder.HasIndex(u => u.NumeroDocumento).IsUnique();

            builder.Property(u => u.TipoDocumento).HasColumnName("tipo_documento");

            builder.Property(u => u.UserName).HasColumnName("username");
            builder.HasIndex(u => u.UserName).IsUnique();

            builder.Property(u => u.Email).HasColumnName("email");
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Password).HasColumnName("password");

            builder.Property(u => u.PrimerNombre).HasColumnName("primer_nombre");
            builder.Property(u => u.SegundoNombre).HasColumnName("segundo_nombre");
            builder.Property(u => u.PrimerApellido).HasColumnName("primer_apellido");
            builder.Property(u => u.SegundoApellido).HasColumnName("segundo_apellido");

            builder.Property(u => u.Estado).HasColumnName("activo");

            builder.Property(u => u.IdRol).HasColumnName("id_rol");

            builder.Property(u => u.FechaRegistro).HasColumnName("fecha_registro");
            builder.Property(u => u.FechaActualizacion).HasColumnName("fecha_actualizacion");
            builder.Property(u => u.FechaDesvinculacion).HasColumnName("fecha_desvinculacion");

            builder.HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
