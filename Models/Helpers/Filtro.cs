using System;
using System.Text;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Tach.Models.Helpers {
    public class Filtro {
        public string Id { get; set; }

        public string Criterio1 { get; set; }

        public string Criterio2 { get; set; }

        public string[] Criterios { get; set; }

        public string Operador { get; set; }

        private dynamic[] VerifyDate(params string[] criterios) {
            var newCriterios = new dynamic[criterios.Length];
            for(var i = 0; i < criterios.Length; i++) {
                newCriterios[i] = this.Id.StartsWith("Fecha") ? DateTime.Parse(criterios[i]) : criterios[i];
            }
            return newCriterios;
        }

        private IQueryable<T> AddCriterios<T>(IQueryable<T> query) {
            var condicion = "";
            for(var i = 0; i < this.Criterios.Count(); i++) {
                condicion += $"{this.Id}.Contains(@{i})";
                if(i < (this.Criterios.Count() - 1)) {
                    condicion += " || ";
                }
            }
            return query.Where(condicion, this.VerifyDate(this.Criterios));
        }

        public IQueryable<T> AddFiltros<T>(IQueryable<T> query) {
            switch(this.Operador) {
                case "between": return query.Where($"{this.Id} >= @0 && {this.Id} <= @1", this.VerifyDate(this.Criterio1, this.Criterio2));
                case "like" : return query.Where($"{this.Id}.Contains(@0)", this.Criterio1);
                case "multiple": return this.AddCriterios<T>(query);
                default: return query.Where($"{this.Id} {this.Operador} @0", this.Criterio1); 
            }
        }

        public dynamic[] AddFiltro(StringBuilder builder, ref int cont) {
            switch(this.Operador) {
                case "between":
                    builder.Append($"{this.Id} >= @{cont} && {this.Id} <= @{cont + 1}").Append("&&");
                    cont = cont + 2;
                    return this.VerifyDate(this.Criterio1, this.Criterio2);
                case "like" : 
                    builder.Append($"{this.Id}.Contains(@{cont})").Append("&&");
                    cont++;
                    return new string[] { this.Criterio1 };
                default: 
                    builder.Append($"{this.Id} {this.Operador} @{cont}").Append("&&");
                    cont++;
                    return new string[] { this.Criterio1 };
            }
        }
    }
}