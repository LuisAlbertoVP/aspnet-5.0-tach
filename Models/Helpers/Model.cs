using System.Collections.Generic;

namespace Tach.Models.Helpers {
    public class Model<T> {

        public ICollection<T> Data { get; set; }

        public int Total { get; set; }
        
    }
}