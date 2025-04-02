using backend_gestorinv.DTOs;
using backend_gestorinv.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_gestorinv.Controllers
{
    [Route("api/logs")]
    [ApiController]
    [Authorize]
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        // Obtener todos los Logs
        [HttpGet]
        public async Task<IActionResult> GetLogs()
        {
            var logs = await _logService.GetLogs();
            return Ok(new { success = true, message = "Logs obtenidos correctamente", data = logs });
        }

        // Registrar un nuevo Log
        [HttpPost]
        public async Task<IActionResult> RegisterLog([FromBody] LogCreateDTO request)
        {
            var result = await _logService.RegisterLog(request);

            if (!result)
            {
                return StatusCode(500, new { message = "No se pudo registrar el log" });
            }

            return Ok(new { message = "Log registrado correctamente" });
        }

        // Eliminar todos los Logs registrados
        [HttpDelete]
        public async Task<IActionResult> DeleteAllLogs()
        {
            var result = await _logService.DeleteAllLogs();

            if (!result)
            {
                return StatusCode(500, new { message = "No se pudieron eliminar los logs" });
            }

            return Ok(new { success = true, message = "Todos los logs han sido eliminados correctamente" });
        }

    }
}
