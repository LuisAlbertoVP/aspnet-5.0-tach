using System.Collections.Generic;

namespace Tach.Models.Entities {
    public class Compra : Transaccion {

        public Proveedor Proveedor { get; set; }

        public ICollection<CompraDetalle> CompraDetalle { get; set; }
        
    }
}