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

        public bool Estado { get; set; }

        public string OperadorLogico { get; set; }

        public async Task<Model> BuildModel<T>(IQueryable<T> query, string fields, bool estadoTabla = true) {
            var builder = new StringBuilder();
            var filtros = new List<dynamic>();
            int cont = 0;
            builder.Append(string.Format("Estado == {0}", this.Estado)).Append("&&");
            if(estadoTabla) {
                builder.Append("EstadoTabla == true").Append("&&");
            }
            builder.Append('(');
            for(var i = 0; i < this.Filtros.Length; i++) {
                if(i > 0) {
                    builder.Append(this.OperadorLogico);
                }
                filtros.AddRange(this.Filtros[i].AddFiltro(builder, ref cont));
            }
            builder.Append(')');
            query = query.Where(builder.ToString(), filtros.ToArray()).OrderBy(this.Orden.ToString());
            var model = new Model();
            model.Total = await query.CountAsync();
            model.Data = await query.Skip(this.Pagina * this.Cantidad).Take(this.Cantidad).Select(fields).ToDynamicListAsync();
            return model;
        }
    }
}