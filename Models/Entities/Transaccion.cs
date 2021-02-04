using System;

namespace Tach.Models.Entities {
    public class Transaccion {

        public string Id { get; set; }

        public int Cantidad { get; set; }

        public double Total { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public string UsuarioIngreso { get; set; }

        public DateTime FechaIngreso { get; set; }
        
    }
}