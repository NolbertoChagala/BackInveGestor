using backend_gestorinv.Services.IServices;
using backend_gestorinv.DTOs.ProductDTO;
using Microsoft.AspNetCore.Mvc;

namespace backend_gestorinv.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // Obtener todos los productos
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _inventoryService.GetProducts();
            return Ok(new { success = true, message = "Productos obtenidos correctamente", data = products });
        }

        // Obtener producto por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _inventoryService.GetById(id);

            if (product == null)
            {
                return NotFound(new { success = false, message = "Producto no encontrado" });
            }

            return Ok(new { success = true, message = "Producto obtenido correctamente", data = product });
        }

        // Crear un nuevo producto
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.producto))
            {
                return BadRequest(new { success = false, message = "El nombre del producto es requerido" });
            }

            var result = await _inventoryService.CreateProduct(request);

            if (!result)
            {
                return StatusCode(500, new { success = false, message = "Error al crear el producto" });
            }

            return Ok(new { success = true, message = "Producto creado correctamente" });
        }

        // Editar producto
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] ProductEditDTO request)
        {
            var result = await _inventoryService.EditProduct(id, request);

            if (!result)
            {
                return NotFound(new { success = false, message = "No se pudo actualizar el producto" });
            }

            return Ok(new { success = true, message = "Producto actualizado correctamente" });
        }

        // Eliminar producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _inventoryService.DeleteProduct(id);

            if (!result)
            {
                return NotFound(new { success = false, message = "No se pudo eliminar el producto" });
            }

            return Ok(new { success = true, message = "Producto eliminado correctamente" });
        }
    }
}
