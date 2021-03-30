using System.Collections.Generic;

namespace Tach.Models.Entities {
    public class Cliente : Persona {

        public string TipoCliente { get; set; }

        public ICollection<Venta> Ventas { get; set; } 
        
    }
}