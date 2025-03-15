using backend_gestorinv.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_gestorinv.Services.IServices
{
   public interface ICategoriaService
   {
       public Task<List<Categoria>> GetCategorias();
       public Task<Categoria> GetById(int id);
       public Task<bool> CreateCategoria(Categoria request);
       public Task<Categoria?> EditCategoria(int id, Categoria categoria);
       public Task<bool> DeleteCategoria(int id);

   }
}
