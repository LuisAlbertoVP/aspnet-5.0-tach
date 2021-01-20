using System;
using System.Text.Json;
using System.Collections.Generic;

namespace Tach.Models.Entities {
    public class Usuario : Entity {
        public string Id { get; set; }

        public string NombreUsuario { get; set; }

        public string Nombres { get; set; }

        public string Clave { get; set; }
        
        public string Cedula { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Correo { get; set; }

        public DateTime FechaContratacion { get; set; }

        public double Salario { get; set; }

        public ICollection<Rol> Roles { get; set; }

        public string ToJSON() {
            var serializeOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(this, serializeOptions);
        }
    }
}