<<<<<<< HEAD
﻿namespace backend_gestorinv.DTOs.MovementDTO
{
    public class DetailsDTO
    {
        public int producto_id { get; set; }
        public string? producto_nombre { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal? total { get; set; }
    }
}
=======
﻿namespace backend_gestorinv.DTOs.MovementDTO
{
    public class DetailsDTO
    {
        public int producto_id { get; set; }
        public string? producto_nombre { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal? total { get; set; }
        public int stock_anterior { get; set; }
        public int stock_nuevo { get; set; }
    }
}
>>>>>>> 15ab1bbd27e5c1b72aef3db6f6f492575701b16c
