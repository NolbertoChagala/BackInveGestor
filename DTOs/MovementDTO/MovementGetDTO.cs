namespace backend_gestorinv.DTOs.MovementDTO
{
    public class MovementGetDTO
    {
        public int id_movimiento {  get; set; }
        public int? usuario_id { get; set; }
        public string usuario_nombre { get; set; }
        public string tipo_movimiento { get; set; }
        public DateTime fecha_registro { get; set; }
    }
}
