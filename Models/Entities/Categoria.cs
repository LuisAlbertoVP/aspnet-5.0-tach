using System.Collections.Generic;

namespace Tach.Models.Entities {
    public class Categoria : Entity {

        public string Id { get; set; }

        public string Descripcion { get; set; }

        public ICollection<Repuesto> Repuestos { get; set; }

        public int Stock { get; set; }

        public double Precio { get; set; }

    }
}