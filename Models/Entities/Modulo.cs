using System.Collections.Generic;

namespace Tach.Models.Entities {
    public class Modulo {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public ICollection<Rol> Roles { get; set; }
    }
}