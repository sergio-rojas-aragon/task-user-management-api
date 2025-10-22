using GTU.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GTU.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<EstadoTarea> EstadoTareas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Rol> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>().HasData(
                new Rol { RolId = 1, Nombre = "Administador" },
                new Rol { RolId = 2, Nombre = "Empleado" },
                new Rol { RolId = 3, Nombre = "Vendedor" }
                );

            modelBuilder.Entity<EstadoTarea>().HasData(
                new EstadoTarea { EstadoTareaId = 1, Nombre="Pendiente" },
                new EstadoTarea { EstadoTareaId = 2, Nombre = "En Proceso" },
                new EstadoTarea { EstadoTareaId = 3, Nombre = "Completada" },
                new EstadoTarea { EstadoTareaId = 4, Nombre = "Cancelada" }
                );

        }


    }
}
