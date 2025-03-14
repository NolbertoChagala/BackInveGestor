using backend_gestorinv.Models.Domain;

namespace backend_gestorinv.Services.IServices
{
    public interface IRolService
    {
        public Task<List<Rol>> GetRoles();
        public Task<Rol> GetById(int id);
        public Task<bool> CreateRol(Rol request);
        public Task<bool> EditRol(int id, Rol rol);
        public Task<bool> DeleteRol(int id);
    }
}
