using System.Collections.Generic;

namespace Tach.Models.Helpers {
    public class Model {

        public ICollection<dynamic> Data { get; set; }

        public int Cantidad { get; set; }

        public int Stock { get; set; }
        
    }
}