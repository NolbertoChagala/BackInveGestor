using backend_gestorinv.Context;
using backend_gestorinv.Models.Domain;
using backend_gestorinv.DTOs.ProductDTO;
using backend_gestorinv.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace backend_gestorinv.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los productos
        public async Task<List<ProductGetDTO>> GetProducts()
        {
            return await _context.Inventario
                .Include(p => p.proveedor)
                .Include(p => p.categoria)
                .Select(p => new ProductGetDTO
                {
                    id_producto = p.id_producto,
                    producto = p.producto,
                    stock = p.stock,
                    proveedor = p.proveedor.proveedor,
                    categoria = p.categoria.categoria,
                    precio_unitario = p.precio_unitario
                })
                .ToListAsync();
        }

        // Obtener producto por ID
        public async Task<Inventario> GetById(int id)
        {
            return await _context.Inventario
                .Include(p => p.proveedor)
                .Include(p => p.categoria)
                .SingleOrDefaultAsync(p => p.id_producto == id);
        }

        // Obtener detalles de producto por ID
        public async Task<ProductDetailsDTO> GetDetailsById(int id)
        {
            var product = await _context.Inventario
                .Include(p => p.categoria)
                .Include(p => p.proveedor)
                .SingleOrDefaultAsync(p => p.id_producto == id);

            if (product == null)
                return null;

            return new ProductDetailsDTO
            {
                id_producto = product.id_producto,
                producto = product.producto,
                stock = product.stock,
                precio_unitario = product.precio_unitario,
                proveedor_id = product.proveedor_id,
                proveedor = product.proveedor?.proveedor,
                categoria_id = product.categoria_id,
                categoria = product.categoria?.categoria 
            };
        }

        // Crear producto
        public async Task<bool> CreateProduct(ProductCreateDTO request)
        {
            try
            {
                var product = new Inventario
                {
                    producto = request.producto,
                    stock = request.stock,
                    precio_unitario = request.precio_unitario,
                    proveedor_id = request.proveedor_id,
                    categoria_id = request.categoria_id
                };

                _context.Inventario.Add(product);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Editar producto
        public async Task<bool> EditProduct(int id, ProductEditDTO request)
        {
            var product = await GetById(id);

            if (product == null)
                return false;

            // Actualizar solo los campos que han cambiado
            if (!string.IsNullOrEmpty(request.producto))
                product.producto = request.producto;

            if (request.stock.HasValue)
                product.stock = request.stock.Value;

            if (request.precio_unitario.HasValue)
                product.precio_unitario = request.precio_unitario.Value;

            if (request.proveedor_id.HasValue)
                product.proveedor_id = request.proveedor_id.Value;

            if (request.categoria_id.HasValue)
                product.categoria_id = request.categoria_id.Value;

            _context.Inventario.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        // Eliminar producto
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await GetById(id);

            if (product == null)
                return false;

            _context.Inventario.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
