using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_gestorinv.Models.Domain
{
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        [ForeignKey("rol")]
        public int? rol_id { get; set; }
        public Rol rol { get; set; }
        public ICollection<MovimientoInventario> movimientos { get; set; }
    }
}
