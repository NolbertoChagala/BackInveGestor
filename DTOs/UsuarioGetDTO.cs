using backend_gestorinv.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend_gestorinv.DTOs
{
    public class UsuarioGetDTO
    {
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string rol { get; set; }
    }
}
