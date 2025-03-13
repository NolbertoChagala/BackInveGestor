using backend_gestorinv.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace backend_gestorinv.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        // Modelos a mapear
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<MovimientoInventario> movimientosInventario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Eliminación en cascada cuando un proveedor se elimina
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.proveedor)
                .WithMany(p => p.productos)
                .HasForeignKey(p => p.proveedor_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.categoria)
                .WithMany()
                .HasForeignKey(p => p.categoria_id)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.rol)
                .WithMany()
                .HasForeignKey(u => u.rol_id) 
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<MovimientoInventario>()
                .HasOne(m => m.producto)
                .WithMany(p => p.movimientos)
                .HasForeignKey(m => m.producto_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovimientoInventario>()
                .HasOne(m => m.usuario)
                .WithMany(u => u.movimientos)
                .HasForeignKey(m => m.usuario_id)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
