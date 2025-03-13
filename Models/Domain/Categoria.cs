using System.ComponentModel.DataAnnotations;

namespace backend_gestorinv.Models.Domain
{
    public class Categoria
    {
        [Key]
        public int id_categoria { get; set; }
        public string categoria { get; set; }
    }
}
