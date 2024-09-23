using Microsoft.EntityFrameworkCore;

namespace UsuarioAPI.Models
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Juan López",
                    NombreDeUsuario = "jlopez",
                    Contrasena = "password"
                },
                new Usuario
                {
                    Id = 2,
                    Nombre = "Pedro Peréz",
                    NombreDeUsuario = "pperez",
                    Contrasena = "password"
                }
            );
        }
    }
}
