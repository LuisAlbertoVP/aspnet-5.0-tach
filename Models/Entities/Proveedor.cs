using System.Text.Json;

namespace Tach.Models.Entities {
    public class Proveedor : Entity {
        public string Id { get; set; }

        public string Descripcion { get; set; }

        public bool Convenio { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string TipoProveedor { get; set; }

        public string Contacto { get; set; }

        public string TelefonoContacto { get; set; }

        public string CorreoContacto { get; set; }

        public string ToJSON() {
            var serializeOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(this, serializeOptions);
        }
    }
}