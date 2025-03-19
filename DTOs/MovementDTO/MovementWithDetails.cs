namespace backend_gestorinv.DTOs.MovementDTO
{
    public class MovementWithDetails
    {
        public int id_movimiento {  get; set; }
        public int? usuario_id { get; set; }
        public string usuario_nombre { get; set; }
        public string tipo_movimiento { get; set; }
        public DateTime fecha_registro { get; set; }
        public List<DetailsDTO> detalles { get; set; } = new List<DetailsDTO>();
    }
}
