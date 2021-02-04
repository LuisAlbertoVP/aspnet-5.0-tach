using System.Collections.Generic;

namespace Tach.Models.Entities {
    public class Venta : Transaccion {

        public Cliente Cliente { get; set; }

        public ICollection<VentaDetalle> VentaDetalle { get; set; }
        
    }
}