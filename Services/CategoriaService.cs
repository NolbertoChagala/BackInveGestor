using backend_gestorinv.Context;
using backend_gestorinv.Models.Domain;
using backend_gestorinv.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_gestorinv.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria> GetById(int id)
        {
            return await _context.Categorias.SingleOrDefaultAsync(c => c.id_categoria == id);
        }

        public async Task<List<Categoria>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<bool> CreateCategoria(Categoria request)
        {
            _context.Categorias.Add(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Categoria?> EditCategoria(int id, Categoria categoria)
        {
            var existingCategoria = await GetById(id);
            if (existingCategoria == null) return null;

            existingCategoria.categoria = categoria.categoria;
            _context.Categorias.Update(existingCategoria);
            await _context.SaveChangesAsync();

            return existingCategoria;
        }

        public async Task<bool> DeleteCategoria(int id)
        {
            var categoria = await GetById(id);
            if (categoria == null) return false;

            _context.Categorias.Remove(categoria);
            return await _context.SaveChangesAsync() > 0;
        }

    }

}
