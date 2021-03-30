using System;

namespace Tach.Models.Entities {
    public class Transaccion : Entity {

        public string Id { get; set; }

        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; }
        
    }
}