namespace Tach.Models.Helpers {
    public class Orden {

        public string Activo { get; set; }

        public string Direccion { get; set; }

        public override string ToString() {
            return this.Activo + " " + this.Direccion;
        }
        
    }
}