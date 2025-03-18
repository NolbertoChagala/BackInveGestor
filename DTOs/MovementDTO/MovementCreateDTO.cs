using backend_gestorinv.Models.Domain;

namespace backend_gestorinv.DTOs.MovementDTO
{
    public class MovementCreateDTO
    {
        public int usuario_id {  get; set; }
        public string tipo_movimiento { get; set; }
        public List<DetailMovementDTO> detalles { get; set; }
    }

    public class DetailMovementDTO
    {
        public int producto_id { get; set; }
        public int cantidad { get; set; }
    }
}
