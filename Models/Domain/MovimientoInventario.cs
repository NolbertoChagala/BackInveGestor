using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_gestorinv.Models.Domain
{
    public class MovimientoInventario
    {
        [Key]
        public int id_movimiento {  get; set; }

        [ForeignKey("usuario")]
        public int? usuario_id { get; set; }
        public Usuario usuario { get; set; }
        public string tipo_movimiento { get; set; }
        public DateTime fecha_registro { get; set; } = DateTime.Now;
        
        public ICollection<DetalleMovimiento> detalles { get; set; }
    }
}
