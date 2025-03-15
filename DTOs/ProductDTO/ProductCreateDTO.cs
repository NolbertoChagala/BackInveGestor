using backend_gestorinv.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_gestorinv.DTOs.ProductDTO
{
    public class ProductCreateDTO
    {
        public string producto { get; set; }
        public int stock { get; set; }
        public decimal precio_unitario { get; set; }
        public int proveedor_id { get; set; }
        public int? categoria_id { get; set; }
    }
}
