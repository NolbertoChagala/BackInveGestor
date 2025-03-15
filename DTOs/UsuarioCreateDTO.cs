using System.ComponentModel.DataAnnotations;

namespace backend_gestorinv.DTOs
{
    public class UsuarioCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public string correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string contraseña { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public int rol_id { get; set; }
    }
}
