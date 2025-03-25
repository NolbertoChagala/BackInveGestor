namespace backend_gestorinv.DTOs
{
    public class LogCreateDTO
    {
        public string mensaje {  get; set; } = string.Empty;
        public string stack_trace {  get; set; } 
        public string endpoint { get; set; } = string.Empty;
        public int status_code { get; set; }
    }
}
