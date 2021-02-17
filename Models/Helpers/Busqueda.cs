using System.Text;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using System.Threading.Tasks;

namespace Tach.Models.Helpers {
    public class Busqueda {
        
        public Filtro[] Filtros { get; set; }

        public Orden Orden { get; set; }

        public int Pagina { get; set; }

        public int Cantidad { get; set; }

        public bool Estado { get; set; }

        public string OperadorLogico { get; set; }

        public async Task<Model> BuildModel<T>(IQueryable<T> queryable, Query query, bool estadoTabla = true) {
            var builder = new StringBuilder();
            var filtros = new List<dynamic>();
            int cont = 0;
            if(estadoTabla) {
                builder.Append("EstadoTabla == true").Append("&&");
            }
            builder.Append(string.Format("Estado == {0}", this.Estado)).Append("&&")
                .Append('(');
            for(var i = 0; i < this.Filtros.Length; i++) {
                if(i > 0) {
                    builder.Append(this.OperadorLogico);
                }
                filtros.AddRange(this.Filtros[i].AddFiltro(builder, ref cont));
            }
            builder.Append(')');
            queryable = queryable.Where(builder.ToString(), filtros.ToArray());
            var model = new Model();
            if(!string.IsNullOrEmpty(query.SumaStock) && !string.IsNullOrEmpty(query.SumaPrecio)) {
                model.Stock = await queryable.SumAsync(query.SumaStock);
                model.Precio = await queryable.SumAsync(query.SumaPrecio);
            }
            model.Cantidad = await queryable.CountAsync();
            model.Data = await queryable
                .OrderBy(this.Orden.ToString())
                .Skip(this.Pagina * this.Cantidad)
                .Take(this.Cantidad)
                .Select(query.CamposConsulta)
                .ToDynamicListAsync();
            return model;
        }
    }
}