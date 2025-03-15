using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_gestorinv.Models.Domain
{
    public class Producto
    {
        [Key]
        public int id_producto { get; set; }
        public string producto {  get; set; }
        public int stock { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal precio { get; set; }

        [ForeignKey("proveedor")]
        public int proveedor_id { get; set; }
        public Proveedor proveedor { get; set; }

        [ForeignKey("categoria")]
        public int? categoria_id { get; set; }
        public Categoria categoria { get; set; }
        public ICollection<MovimientoInventario> movimientos { get; set; }

    }
}
