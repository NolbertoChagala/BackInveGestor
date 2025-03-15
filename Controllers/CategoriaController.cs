using backend_gestorinv.Models.Domain;
using backend_gestorinv.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_gestorinv.Controllers
{

    [Controller]
    [Route("api/categoria")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController (ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        //Obtener todas las categorias
        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _categoriaService.GetCategorias();
            return Ok(new { success = true, message = "Categorias obtenidas correctamente", data = categorias });
        }

        // Obtenemos las categorias por el ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _categoriaService.GetById(id);

            if (categoria == null)
            {
                return NotFound(new { success = false, message = "Categoria no encontrada" });
            }
            return Ok(new { success = true, message = "Categoria obtenida correctamente", data = categoria });
        }

        // Crear una nueva categoria
        [HttpPost]
        public async Task<IActionResult> CreateCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null || string.IsNullOrEmpty(categoria.categoria))
            {
                return BadRequest(new { success = false, message = "La categoria es requerida" });
            }

            var result = await _categoriaService.CreateCategoria(categoria);

            if (!result)
            {
                return StatusCode(500, new { success = false, message = "Error al crear la categoria" });
            }

            return Ok(new { success = true, message = "Categoria creada correctamente"});
        }


        // Editar categoria
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategoria(int id, [FromBody] Categoria categoria)
        {
           if(categoria == null || string.IsNullOrEmpty(categoria.categoria))
           {
               return BadRequest(new { success = false, message = "La categoria es requerida" });
           }

           var updatecategoria = await _categoriaService.EditCategoria(id, categoria);

            if(updatecategoria == null)
            {
                return NotFound(new { success = false, message = "Error al editar la categoria" });
            }
            return Ok(new { success = true, message = "Categoria editada correctamente", categoria = updatecategoria });
        }


        // Eliminar categoria
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var result  = await _categoriaService.DeleteCategoria(id);
            if(!result)
            {
                return NotFound(new { success = false, message = "Error al eliminar la categoria" });
            }
            return Ok(new { success = true, message = "Categoria eliminada correctamente" });
        }
    }
}
