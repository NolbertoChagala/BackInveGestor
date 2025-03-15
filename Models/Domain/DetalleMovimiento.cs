using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_gestorinv.Models.Domain
{
    public class DetalleMovimiento
    {
        [Key]
        public int id_detalle { get; set; }

        [ForeignKey("movimiento")]
        public int movimiento_id { get; set; }
        public MovimientoInventario movimiento { get; set; }
        [ForeignKey("producto")]
        public int producto_id { get; set; }
        public Inventario producto { get; set; }
        public int cantidad {  get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal precio_unitario { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? total { get; set; }
    }
}
