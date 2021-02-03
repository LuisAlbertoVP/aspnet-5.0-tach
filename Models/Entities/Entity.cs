using System;

namespace Tach.Models.Entities {
    public class Entity {

        public bool Estado { get; set; }

        public bool EstadoTabla { get; set; }

        public string UsuarioIngreso { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
        
    }
}