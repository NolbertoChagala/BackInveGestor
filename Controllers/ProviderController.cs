using backend_gestorinv.DTOs.ProviderDTO;
using backend_gestorinv.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace backend_gestorinv.Controllers
{
    [Route("api/providers")]
    [ApiController]
    public class ProviderController : Controller
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        // Obtener todos los proveedores
        [HttpGet]
        public async Task<IActionResult> GetProviders()
        {
            var providers = await _providerService.GetProviders();
            return Ok(new { success = true, message = "Proveedores obtenidos correctamente", data = providers });
        }

        // Obtener proveedor por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProviderById(int id)
        {
            var provider = await _providerService.GetById(id);

            if (provider == null)
            {
                return NotFound(new { success = false, message = "Proveedor no encontrado" });
            }

            return Ok(new { success = true, message = "Proveedor obtenido correctamente", data = provider });
        }

        // Crear un proveedor
        [HttpPost]
        public async Task<IActionResult> CreateProvider([FromBody] ProviderCreateDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.proveedor))
            {
                return BadRequest(new { success = false, message = "El proveedor es requerido" });
            }

            var result = await _providerService.CreateProvider(request);

            if (!result)
            {
                return StatusCode(500, new { success = false, message = "Error al crear el proveedor" });
            }

            return Ok(new { success = true, message = "Proveedor creado correctamente" });
        }

        // Editar un proveedor
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProvider(int id, [FromBody] ProviderUpdateDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.proveedor))
            {
                return BadRequest(new { success = false, message = "El proveedor es requerido" });
            }

            var result = await _providerService.EditProvider(id, request);

            if (!result)
            {
                return NotFound(new { success = false, message = "No se pudo actualizar el proveedor" });
            }

            return Ok(new { success = true, message = "Proveedor actualizado correctamente" });
        }

        // Eliminar un proveedor
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var result = await _providerService.DeleteProvider(id);

            if (!result)
            {
                return NotFound(new { success = false, message = "No se pudo eliminar el proveedor" });
            }

            return Ok(new { success = true, message = "Proveedor eliminado correctamente" });
        }
    }
}
