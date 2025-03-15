using backend_gestorinv.Models.Domain;
using backend_gestorinv.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using backend_gestorinv.DTOs;

namespace backend_gestorinv.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IRolService _rolService;

        public UsuarioController(IUsuarioService usuarioService, IRolService rolService)
        {
            _usuarioService = usuarioService;
            _rolService = rolService;
        }

        // Obtener todos los usuarios
        [HttpGet("ObtenerUsuarios")]
        public IActionResult ObtenerUsuarios()
        {
            try
            {
                var result = _usuarioService.GetUsuarios();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno en el servidor", error = ex.Message });
            }
        }

        // Obtener roles para crear un nuevo usuario
        [HttpGet("Crear")]
        public async Task<IActionResult> ObtenerRolesParaCrearUsuario()
        {
            try
            {
                List<Rol> result = await _rolService.GetRoles();
                var roles = result.Select(p => new SelectListItem()
                {
                    Text = p.rol,
                    Value = p.id_rol.ToString()
                });
                return Ok(new { roles });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno en el servidor", error = ex.Message });
            }
        }

        // Crear Usuario
        [HttpPost("Crear")]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioCreateDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Los datos enviados no son válidos", errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            try
            {
                // Llamar al servicio para crear el usuario con el DTO
                bool result = await _usuarioService.CreateUsuario(request);

                if (result)
                {
                    return CreatedAtAction(nameof(ObtenerUsuarios), new { id = request.rol_id }, request);
                }

                return StatusCode(500, new { message = "Hubo un problema al crear el usuario" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno en el servidor", error = ex.Message });
            }
        }

        // Obtener un usuario para editar
        [HttpGet("Editar/{id}")]
        public async Task<IActionResult> EditarUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioById(id);
                if (usuario == null)
                {
                    return NotFound(new { message = "Usuario no encontrado" });
                }

                List<Rol> roles = await _rolService.GetRoles();
                var rolesList = roles.Select(p => new SelectListItem()
                {
                    Text = p.rol,
                    Value = p.id_rol.ToString(),
                    Selected = p.id_rol == usuario.rol_id // Verifica correctamente el rol seleccionado
                });

                return Ok(new { usuario, roles = rolesList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        // Editar usuario
        [HttpPut("Editar/{id_usuario}")]
        public async Task<IActionResult> EditarUsuario(int id_usuario, [FromBody] UsuarioEditDTO request)
        {
            try
            {
                bool actualizado = await _usuarioService.EditUsuario(id_usuario, request);
                if (!actualizado)
                {
                    return BadRequest(new { message = "No se puede actualizar el usuario" });
                }
                return Ok(new { message = "Usuario actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno en el servidor", error = ex.Message });
            }
        }

        // Eliminar usuario
        [HttpDelete("Eliminar/{id_usuario}")]
        public async Task<IActionResult> EliminarUsuario(int id_usuario)
        {
            try
            {
                bool result = await _usuarioService.DeleteUsuario(id_usuario);
                if (result)
                {
                    return Ok(new { success = true, message = "Usuario eliminado exitosamente" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Error al eliminar el usuario" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno en el servidor", error = ex.Message });
            }
        }
    }
}
