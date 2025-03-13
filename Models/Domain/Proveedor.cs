using System.ComponentModel.DataAnnotations;

namespace backend_gestorinv.Models.Domain
{
    public class Proveedor
    {
        [Key]
        public int id_proveedor { get; set; }
        public string proveedor { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public ICollection<Producto> productos { get; set; }
    }
}
