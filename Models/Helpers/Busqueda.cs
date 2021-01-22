using System.Text;
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
            var builder = new StringBuilder();
            var filtros = new List<dynamic>();
            if(this.Estado == "2") {
                builder.Append("Estado == true || Estado == false").Append("&&");
            } else {
                builder.Append(string.Format("Estado == {0} &&", this.Estado == "1" ? true : false)).Append("&&");
            }
            int cont = 0;
            for(var i = 0; i < this.Filtros.Count(); i++) {
                //query = this.Filtros[i].AddFiltro<T>(query);
                filtros.AddRange(this.Filtros[i].AddFiltro(builder, ref cont));
            }
            builder.Append("EstadoTabla == true");
            query = query.Where(builder.ToString(), filtros.ToArray()).OrderBy(this.Orden.ToString());
            var model = new Model<T>();
            model.Total = await query.CountAsync();
            model.Data = await query.Skip(this.Pagina * this.Cantidad).Take(this.Cantidad).Select<T>(fields).ToListAsync();
            return model;
        }
    }
}