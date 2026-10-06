using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Infrastructure.Persistence.Configurations
{
    public class MateriaConfiguration : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {

            builder.ToTable("materia");

            builder.HasKey(m => m.IdMateria);
            builder.Property(m => m.IdMateria).HasColumnName("id_materia");

            builder.Property(m => m.CodMateria).HasColumnName("cod_materia").HasConversion<string>();
            builder.HasIndex(m => m.CodMateria).IsUnique();

            builder.Property(m => m.NombreMateria).HasColumnName("nombre_materia");
            builder.HasIndex(m => m.NombreMateria).IsUnique();

            builder.Property(m => m.Creditos).HasColumnName("creditos");

            builder.Property(m => m.IdProfesor).HasColumnName("id_profesor");

            builder.Property(m => m.FechaRegistro).HasColumnName("fecha_registro");

            builder.Property(m => m.FechaActualizacion).HasColumnName("fecha_actualizacion");

            builder.HasOne(m => m.Profesor)
                   .WithMany(p => p.Materias)
                   .HasForeignKey(m => m.IdProfesor)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
