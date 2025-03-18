using backend_gestorinv.DTOs.MovementDTO;

namespace backend_gestorinv.Services.IServices
{
    public interface IMovementService
    {
        public Task<int> CreateMovement(MovementCreateDTO movementDTO, List<DetailMovementDTO> details);
        public Task AddMovementDetails(int movementId, string movementType, List<DetailMovementDTO> details);
        public Task UpdateStock(string movementType, List<DetailMovementDTO> details);
        public Task<List<MovementGetDTO>> GetAllMovements();
    }
}
