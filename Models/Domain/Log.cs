using System.ComponentModel.DataAnnotations;

namespace backend_gestorinv.Models.Domain
{
    public class Log
    {
        [Key]
        public int id_log {  get; set; }
        public string mensaje { get; set; } = string.Empty;
        public string? stack_trace { get; set; }
        public string endpoint { get; set; } = string.Empty;
        public int status_code { get; set; }
        public DateTime fecha_registro { get; set; } = DateTime.Now;
    }
}
