using backend_gestorinv.DTOs.MovementDTO;
using backend_gestorinv.Services;
using backend_gestorinv.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace backend_gestorinv.Controllers
{
    [Route("api/movements")]
    [ApiController]
    public class MovementController : Controller
    {
        private readonly IMovementService _movementService;

        public MovementController(IMovementService movementService)
        {
            _movementService = movementService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterMovement([FromBody] MovementCreateDTO movementDTO)
        {
            try
            {
                if (movementDTO == null || movementDTO.detalles == null || !movementDTO.detalles.Any())
                    return BadRequest(new { message = "Los datos del movimiento son inválidos o están incompletos." });

                // 1. Crear el movimiento
                int movementId = await _movementService.CreateMovement(movementDTO, movementDTO.detalles);

                return Ok(new { message = "Movimiento registrado correctamente.", movementId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al registrar el movimiento: {ex.Message}" });
            }
        }

    }
}
