using System.Collections.Generic;

namespace Tach.Models.Entities {
    public class Rol : Entity {

        public string Id { get; set; }

        public string Descripcion { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }

        public ICollection<Modulo> Modulos { get; set; }

    }
}