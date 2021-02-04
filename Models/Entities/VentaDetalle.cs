namespace Tach.Models.Entities {
    public class VentaDetalle {

        public int Cantidad { get; set; }

        public string RepuestoId { get; set; }

        public Repuesto Repuesto { get; set; }

        public string VentaId { get; set; }

        public Venta Venta { get; set; }
        
    }
}