using System;

namespace Tach.Models.Entities {
    public class Persona : Entity {

        public string Id { get; set; }

        public string Nombres { get; set; }

        public string Cedula { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Correo { get; set; }

    }
}