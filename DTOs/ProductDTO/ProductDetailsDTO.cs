namespace backend_gestorinv.DTOs.ProductDTO
{
    public class ProductDetailsDTO
    {
        public int id_producto { get; set; }
        public string producto { get; set; }
        public int stock { get; set; }
        public decimal precio_unitario { get; set; }
        public int proveedor_id { get; set; }
        public string proveedor { get; set; }
        public int? categoria_id { get; set; }
        public string categoria { get; set; }
    }
}
