using backend_gestorinv.Models.Domain;

namespace backend_gestorinv.DTOs.ProviderDTO
{
    public class ProviderGetDTO
    {
        public int id_proveedor { get; set; }
        public string proveedor { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public ICollection<ProductsDTO> productos { get; set; }
    }
}
