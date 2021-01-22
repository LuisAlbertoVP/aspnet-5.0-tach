using System;
using System.Text;

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

        private string[] AddCriterios(StringBuilder builder, ref int cont) {
            builder.Append('(');
            for(var i = 0; i < this.Criterios.Length; i++) {
                builder.Append($"{this.Id}.Contains(@{cont})");
                if(i < (this.Criterios.Length - 1)) {
                    builder.Append("||");
                }
                cont++;
            }
            builder.Append(')');
            return this.Criterios;
        }

        public dynamic[] AddFiltro(StringBuilder builder, ref int cont) {
            switch(this.Operador) {
                case "multiple":
                    return this.AddCriterios(builder, ref cont);
                case "between":
                    builder.Append($"({this.Id} >= @{cont} && {this.Id} <= @{cont + 1})");
                    cont = cont + 2;
                    return this.VerifyDate(this.Criterio1, this.Criterio2);
                case "like" : 
                    builder.Append($"{this.Id}.Contains(@{cont})");
                    cont++;
                    return new string[] { this.Criterio1 };
                default: 
                    builder.Append($"{this.Id} {this.Operador} @{cont}");
                    cont++;
                    return new string[] { this.Criterio1 };
            }
        }
    }
}