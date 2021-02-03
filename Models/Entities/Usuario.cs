using System;
using System.Collections.Generic;

namespace Tach.Models.Entities {
    public class Usuario : Persona {

        public string NombreUsuario { get; set; }

        public string Clave { get; set; }

        public DateTime FechaContratacion { get; set; }

        public double Salario { get; set; }

        public ICollection<Rol> Roles { get; set; }

    }
}