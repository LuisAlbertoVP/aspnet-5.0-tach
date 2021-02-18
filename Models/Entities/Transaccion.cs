using System;

namespace Tach.Models.Entities {
    public class Transaccion : Entity {

        public string Id { get; set; }

        public DateTime Fecha { get; set; }

        public int Cantidad { get; set; }

        public double Total { get; set; }

        public string Descripcion { get; set; }
        
    }
}