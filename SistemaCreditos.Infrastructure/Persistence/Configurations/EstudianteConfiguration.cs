using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Infrastructure.Persistence.Configurations
{
    public class EstudianteConfiguration : IEntityTypeConfiguration<Estudiante>
    {
        public void Configure(EntityTypeBuilder<Estudiante> builder)
        {
            builder.ToTable("estudiante");

            builder.HasKey(e => e.IdEstudiante);
            builder.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");

            builder.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            builder.HasIndex(e => e.IdUsuario).IsUnique();

            builder.HasOne(e => e.Usuario)
                   .WithOne(u => u.Estudiante)
                   .HasForeignKey<Estudiante>(e => e.IdUsuario)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}