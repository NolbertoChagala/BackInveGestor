using backend_gestorinv.Context;
using backend_gestorinv.DTOs.ProviderDTO;
using backend_gestorinv.Models.Domain;
using backend_gestorinv.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace backend_gestorinv.Services
{
    public class ProviderService : IProviderService
    {
        private readonly AppDbContext _context;
        public ProviderService(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los proveedores
        public async Task<List<Proveedor>> GetProviders()
        {
            return await _context.Proveedores.ToListAsync();
        }

        // Obtener proveedor por ID
        public async Task<Proveedor> GetById(int id)
        {
            return await _context.Proveedores.Include(p => p.productos).SingleOrDefaultAsync(p => p.id_proveedor == id);
        }

        // Crear un proveedor
        public async Task<bool> CreateProvider(ProviderCreateDTO request)
        {
            try
            {
                var proveedor = new Proveedor
                {
                    proveedor = request.proveedor,
                    telefono = request.telefono,
                    correo = request.correo,
                    direccion = request.direccion
                };

                _context.Proveedores.Add(proveedor);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Editar un proveedor
        public async Task<bool> EditProvider(int id, ProviderUpdateDTO request)
        {
            var proveedor = await GetById(id);

            if (proveedor == null)
                return false;

            if (!string.IsNullOrEmpty(request.proveedor))
                proveedor.proveedor = request.proveedor;

            if (!string.IsNullOrEmpty(request.telefono))
                proveedor.telefono = request.telefono;

            if (!string.IsNullOrEmpty(request.correo))
                proveedor.correo = request.correo;

            if (!string.IsNullOrEmpty(request.direccion))
                proveedor.direccion = request.direccion;

            _context.Proveedores.Update(proveedor);
            return await _context.SaveChangesAsync() > 0;
        }

        // Eliminar un proveedor
        public async Task<bool> DeleteProvider(int id)
        {
            var proveedor = await GetById(id);

            if (proveedor == null)
                return false;

            _context.Proveedores.Remove(proveedor);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
