using System.Collections.Generic;

namespace Tach.Models.Entities {
    public class Repuesto : Entity {

        public string Id { get; set; }

        public string Codigo { get; set; }

        public Marca Marca { get; set; }

        public Categoria Categoria { get; set; }

        public string Modelo { get; set; }

        public string Epoca { get; set; }

        public string SubMarca { get; set; }

        public int Stock { get; set; }

        public double Precio { get; set; }

        public string Descripcion { get; set; }

        public ICollection<Compra> Compras { get; set; }

        public ICollection<Venta> Ventas { get; set; }

    }
}