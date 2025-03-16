using backend_gestorinv.DTOs.ProductDTO;
using backend_gestorinv.Models.Domain;

namespace backend_gestorinv.Services.IServices
{
    public interface IInventoryService
    {
        public Task<List<ProductGetDTO>> GetProducts();
        public Task<Inventario> GetById(int id);
        public Task<ProductDetailsDTO> GetDetailsById(int id);
        public Task<bool> CreateProduct(ProductCreateDTO request);
        public Task<bool> EditProduct(int id, ProductEditDTO request);    
        public Task<bool> DeleteProduct(int id);
    }
}
