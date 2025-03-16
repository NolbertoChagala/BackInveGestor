using backend_gestorinv.DTOs.ProviderDTO;
using backend_gestorinv.Models.Domain;

namespace backend_gestorinv.Services.IServices
{
    public interface IProviderService
    {
        public Task<List<Proveedor>> GetProviders();
        public Task<Proveedor> GetById(int id);
        public Task<ProviderGetDTO> GetProviderWithProductsById(int id);
        public Task<bool> CreateProvider(ProviderCreateDTO request);
        public Task<bool> EditProvider(int id, ProviderUpdateDTO request);
        public Task<bool> DeleteProvider(int id);
    }
}
