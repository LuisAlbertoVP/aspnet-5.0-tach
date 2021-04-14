namespace Tach.Models.Entities {
    public class CompraDetalle {

        public int Cantidad { get; set; }

        public double Precio { get; set; }

        public string Notas { get; set; }

        public string CompraId { get; set; }

        public Compra Compra { get; set; }

        public string RepuestoId { get; set; }

        public Repuesto Repuesto { get; set; }
        
    }
}