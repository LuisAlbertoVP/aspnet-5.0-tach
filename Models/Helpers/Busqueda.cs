using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Tach.Models.Helpers {
    public class Busqueda {
        public Filtro[] Filtros { get; set; }

        public Orden Orden { get; set; }

        public int Pagina { get; set; }

        public int Cantidad { get; set; }

        public string Estado { get; set; }

        public async Task<Model<T>> BuildModel<T>(IQueryable<T> query, string fields) {
            if(this.Estado == "2") {
                query = query.Where("Estado == true || Estado == false");
            } else {
                query = query.Where("Estado == @0", this.Estado == "1" ? true : false);
            }
            for(var i = 0; i < this.Filtros.Count(); i++) {
                query = this.Filtros[i].AddFiltro<T>(query);
            }
            query = query.Where("EstadoTabla == true").OrderBy(this.Orden.ToString());
            var model = new Model<T>();
            model.Total = await query.CountAsync();
            model.Data = await query.Skip(this.Pagina * this.Cantidad).Take(this.Cantidad).Select<T>(fields).ToListAsync();
            return model;
        }
    }
}