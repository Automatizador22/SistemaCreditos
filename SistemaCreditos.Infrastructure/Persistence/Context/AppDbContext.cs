
using Microsoft.EntityFrameworkCore;
using SistemaCreditos.Domain.Entities;


namespace SistemaCreditos.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

    }
}
