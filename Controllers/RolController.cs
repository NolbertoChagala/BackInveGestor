using backend_gestorinv.Models.Domain;
using backend_gestorinv.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace backend_gestorinv.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolController : Controller
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        // Obtener todos los roles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _rolService.GetRoles();
            return Ok(new { success = true, message = "Roles obtenidos correctamente", data = roles });
        }

        // Crear un nuevo rol
        [HttpPost]
        public async Task<IActionResult> CreateRol([FromBody] Rol rol)
        {
            if (rol == null || string.IsNullOrEmpty(rol.rol))
            {
                return BadRequest(new { success = false, message = "El rol es requerido" });
            }

            var result = await _rolService.CreateRol(rol);

            if (!result)
            {
                return StatusCode(500, new { success = false, message = "Error al crear el rol" });
            }

            return Ok(new { success = true, message = "Rol creado correctamente" });
        }

        // Obtener rol por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolById(int id)
        {
            var rol = await _rolService.GetById(id);

            if (rol == null)
            {
                return NotFound(new { success = false, message = "Rol no encontrado" });
            }

            return Ok(new { success = true, message = "Rol obtenido correctamente", data = rol });
        }

        // Editar rol
        [HttpPut("{id}")]
        public async Task<IActionResult> EditRol(int id, [FromBody] Rol rol)
        {
            if (rol == null || string.IsNullOrEmpty(rol.rol))
            {
                return BadRequest(new { success = false, message = "El rol es requerido" });
            }

            var result = await _rolService.EditRol(id, rol);

            if (!result)
            {
                return NotFound(new { success = false, message = "No se pudo actualizar el rol" });
            }

            return Ok(new { success = true, message = "Rol actualizado correctamente" });
        }

        // Eliminar rol
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var result = await _rolService.DeleteRol(id);

            if (!result)
            {
                return NotFound(new { success = false, message = "No se pudo eliminar el rol" });
            }

            return Ok(new { success = true, message = "Rol eliminado correctamente" });
        }
    }
}
