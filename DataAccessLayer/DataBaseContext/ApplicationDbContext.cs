using EntitiesLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();

        public DbSet<Animal> Animales => Set<Animal>();

        public DbSet<Aviso> Avisos => Set<Aviso>();

        public DbSet<Comentario> Comentarios => Set<Comentario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .HasOne(animal => animal.Usuario)
                .WithMany(usuario => usuario.Animales)
                .HasForeignKey(animal => animal.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Aviso>()
                .HasOne(aviso => aviso.Animal)
                .WithMany(animal => animal.Avisos)
                .HasForeignKey(aviso => aviso.IdAnimal)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comentario>()
                .HasOne(comentario => comentario.Usuario)
                .WithMany(usuario => usuario.Comentarios)
                .HasForeignKey(comentario => comentario.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comentario>()
                .HasOne(comentario => comentario.Aviso)
                .WithMany(aviso => aviso.Comentarios)
                .HasForeignKey(comentario => comentario.IdAviso)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
