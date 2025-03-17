using backend_gestorinv.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace backend_gestorinv.DTOs
{
    public class UsuarioEditDTO
    {
        public string nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public string correo { get; set; }

        public string contraseña { get; set; } 

        [Required(ErrorMessage = "El rol es obligatorio.")]

        public int rol_id { get; set; }
    }
}