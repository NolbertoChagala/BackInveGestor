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
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<MovimientoInventario> Movimientos_Inventario { get; set; }
        public DbSet<DetalleMovimiento> Detalles_Movimiento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Eliminación en cascada cuando un proveedor se elimina
            modelBuilder.Entity<Inventario>()
                .HasOne(p => p.proveedor)
                .WithMany(p => p.productos)
                .HasForeignKey(p => p.proveedor_id)
                .OnDelete(DeleteBehavior.Cascade);

            // Deja la categoría nula si se elimina
            modelBuilder.Entity<Inventario>()
                .HasOne(p => p.categoria)
                .WithMany()
                .HasForeignKey(p => p.categoria_id)
                .OnDelete(DeleteBehavior.SetNull);

            // Deja el rol nulo si se elimina
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.rol)
                .WithMany()
                .HasForeignKey(u => u.rol_id) 
                .OnDelete(DeleteBehavior.SetNull);

            // Deja el usuario nulo si se elimina
            modelBuilder.Entity<MovimientoInventario>()
                .HasOne(m => m.usuario)
                .WithMany(u => u.movimientos)
                .HasForeignKey(m => m.usuario_id)
                .OnDelete(DeleteBehavior.SetNull);

            // MovimientoInventario - DetalleMovimiento (1 a N)
            modelBuilder.Entity<DetalleMovimiento>()
                .HasOne(d => d.movimiento)
                .WithMany(m => m.detalles)
                .HasForeignKey(d => d.movimiento_id)
                .OnDelete(DeleteBehavior.Cascade); // Si se elimina el movimiento, se eliminan sus detalles.

            // DetalleMovimiento - Producto (N a 1)
            modelBuilder.Entity<DetalleMovimiento>()
                .HasOne(d => d.producto)
                .WithMany(p => p.detalles_movimiento)
                .HasForeignKey(d => d.producto_id)
                .OnDelete(DeleteBehavior.Restrict); // Si se elimina el producto, no se eliminan los detalles, pero el stock queda registrado.
        }
    }
}
