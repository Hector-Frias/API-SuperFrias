using Microsoft.EntityFrameworkCore;
using SuperFrias.Model;

namespace SuperFrias.Contexts
{
    public class ConexionSQLServer:DbContext

    {
        public ConexionSQLServer()
        {
        }

        public ConexionSQLServer(DbContextOptions<ConexionSQLServer> options ) : base(options){}

        public DbSet<Producto> Producto { get; set; }
      
        public DbSet<Pedidos> Pedidos { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Categoria> Categoria { get; set; }
        
        public DbSet<DetallesPedidos>  DetallesPedidos { get; set; }    



    }
}
