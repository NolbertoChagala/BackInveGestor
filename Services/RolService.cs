using backend_gestorinv.Context;
using backend_gestorinv.Models.Domain;
using backend_gestorinv.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace backend_gestorinv.Services
{
    public class RolService : IRolService
    {
        private readonly AppDbContext _context;

        public RolService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Rol> GetById(int id)
        {
            return await _context.Roles.SingleOrDefaultAsync(r => r.id_rol == id);
        }

        public async Task<bool> CreateRol(Rol request)
        {
            _context.Roles.Add(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditRol(int id, Rol rol)
        {
            var existingRol = await GetById(id);
            if (existingRol == null) return false;

            existingRol.rol = rol.rol;
            _context.Roles.Update(existingRol);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRol(int id)
        {
            var rol = await GetById(id);
            if (rol == null) return false;

            _context.Roles.Remove(rol);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
