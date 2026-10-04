using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Infrastructure.Persistence.Configurations
{
    public class ProfesorConfiguration : IEntityTypeConfiguration<Profesor>
    {
        public void Configure(EntityTypeBuilder<Profesor> builder)
        {
            builder.ToTable("profesor");

            builder.HasKey(p => p.IdProfesor);
            builder.Property(p => p.IdProfesor).HasColumnName("id_profesor");

            builder.Property(p => p.IdUsuario).HasColumnName("id_usuario");
            builder.HasIndex(p => p.IdUsuario).IsUnique();

            builder.Property(p => p.FechaRegistro).HasColumnName("fecha_registro");
            builder.Property(p => p.FechaActualizacion).HasColumnName("fecha_actualizacion");

            builder.Property(p => p.EstadoRegistro)
                   .HasColumnName("estado_registro")
                   .HasConversion<string>()
                   .HasMaxLength(20);

            builder.HasOne(p => p.Usuario)
                   .WithOne(u => u.Profesor)
                   .HasForeignKey<Profesor>(p => p.IdUsuario)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
