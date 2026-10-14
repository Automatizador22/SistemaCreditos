

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaCreditos.Domain.Entities;

namespace SistemaCreditos.Infrastructure.Persistence.Configurations
{
    public class InscripcionConfiguration : IEntityTypeConfiguration<Inscripcion>
    {
        public void Configure(EntityTypeBuilder<Inscripcion> builder)
        {
            //Table Mapping
            builder.ToTable("inscripcion");

            //Primary Key
            builder.HasKey(i => i.IdInscripcion);
            builder.Property(builder => builder.IdInscripcion).HasColumnName("id_inscripcion");

            //Mapeo de Columnas
            builder.Property(i => i.IdEstudiante).HasColumnName("id_estudiante");
            builder.Property(i => i.IdMateria).HasColumnName("id_materia");
            builder.Property(i => i.FechaInscripcion).HasColumnName("fecha_inscripcion");

            //UNIQUE KEY
            builder.HasIndex(i => new { i.IdEstudiante, i.IdMateria }).IsUnique().HasDatabaseName("uk_estudiante_materia");

            //Foreign Key Relationships
            builder.HasOne(i => i.Estudiante)
                .WithMany(e => e.Inscripciones)
                .HasForeignKey(i => i.IdEstudiante)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Materia)
                .WithMany(m => m.Inscripciones)
                .HasForeignKey(i => i.IdMateria)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
