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

    }
}