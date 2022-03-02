using Microsoft.EntityFrameworkCore;
using WebApiCanciones.Entidades;

namespace WebApiCanciones
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cancion> Canciones { get; set; } //Crea tabla en base de datos (la tabla se llama Canciones)
        public DbSet<Album> Albumes { get; set; }
    }
}
