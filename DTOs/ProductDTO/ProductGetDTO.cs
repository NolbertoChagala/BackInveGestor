using backend_gestorinv.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_gestorinv.DTOs.ProductDTO
{
    public class ProductGetDTO
    {
        public int id_producto { get; set; }
        public string producto { get; set; }
        public int stock { get; set; }
        public decimal precio_unitario { get; set; }
        public string proveedor { get; set; }
        public string categoria { get; set; }
    }
}
