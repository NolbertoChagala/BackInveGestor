using backend_gestorinv.Context;
using backend_gestorinv.DTOs;
using backend_gestorinv.Models.Domain;
using backend_gestorinv.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace backend_gestorinv.Services
{
    public class LogService : ILogService
    {
        private readonly AppDbContext _context;

        public LogService(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los Logs
        public async Task<List<Log>> GetLogs()
        {
            return await _context.Logs.OrderByDescending(l => l.fecha_registro).ToListAsync();
        }

        // Registrar Log
        public async Task<bool> RegisterLog(LogCreateDTO request)
        {
            try
            {
                var log = new Log
                {
                    mensaje = request.mensaje,
                    stack_trace = request.stack_trace,
                    endpoint = request.endpoint,
                    status_code = request.status_code
                };

                _context.Logs.Add(log);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Eliminar todos los Logs
        public async Task<bool> DeleteAllLogs()
        {
            try
            {
                _context.Logs.RemoveRange(_context.Logs);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
