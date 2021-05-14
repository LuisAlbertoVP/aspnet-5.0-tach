using System.Collections.Generic;

namespace Tach.Models.Entities {
    public class Compra : Transaccion {

        public Proveedor Proveedor { get; set; }

        public string TipoDocumento { get; set; }

        public string Numero { get; set; }

        public string Orden { get; set; }

        public string Vendedor { get; set; }

        public string SoldTo { get; set; }

        public string ShipTo { get; set; }

        public string Ruta { get; set; }

        public ICollection<CompraDetalle> CompraDetalle { get; set; }
        
    }
}