using System.ComponentModel.DataAnnotations;

namespace backend_gestorinv.Models.Domain
{
    public class Rol
    {
        [Key]
        public int id_rol { get; set; }
        public string rol { get; set; }
    }
}
