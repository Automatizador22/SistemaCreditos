using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Infrastructure.Persistence.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("rol");

            builder.HasKey(r => r.IdRol);
            builder.Property(r => r.IdRol).HasColumnName("id_rol");

            builder.Property(r => r.Nombre).HasColumnName("nombre");
            builder.HasIndex(r => r.Nombre).IsUnique();

            builder.Property(r => r.Estado).HasColumnName("activo");

            builder.Property(r => r.FechaRegistro).HasColumnName("fecha_registro");
        }
    }
}