namespace backend_gestorinv.DTOs.MovementDTO
{
    public class DetailsDTO
    {
        public int producto_id { get; set; }
        public string? producto_nombre { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal? total { get; set; }
    }
}
