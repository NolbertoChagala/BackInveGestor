namespace backend_gestorinv.DTOs.ProviderDTO
{
    public class ProductsDTO
    {
        public int id_producto { get; set; }
        public string producto { get; set; }
        public int stock { get; set; }
        public decimal precio_unitario { get; set; }
        public string categoria { get; set; }
    }
}
