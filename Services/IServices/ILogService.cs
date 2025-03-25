using backend_gestorinv.DTOs;
using backend_gestorinv.Models.Domain;

namespace backend_gestorinv.Services.IServices
{
    public interface ILogService
    {
        public Task<List<Log>> GetLogs();
        public Task<bool> RegisterLog(LogCreateDTO request);
        public Task<bool> DeleteAllLogs();
    }
}
