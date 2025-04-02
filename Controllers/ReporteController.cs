using backend_gestorinv.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_gestorinv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public ReportController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetLowStockReport()
        {
            var report = await _inventoryService.GenerateLowStockReport();
            if (report == null || report.Length == 0)
            {
                return NotFound("No hay productos con bajo stock.");
            }

            return File(report, "application/pdf", "LowStockReport.pdf");
        }
    }
}
