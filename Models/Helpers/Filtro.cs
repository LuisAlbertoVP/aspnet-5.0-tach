using System;
using System.Text;

namespace Tach.Models.Helpers {
    public class Filtro {
        
        public string Id { get; set; }

        public string Criterio1 { get; set; }

        public string Criterio2 { get; set; }

        public string[] Criterios { get; set; }

        public string Condicion { get; set; }

        public dynamic[] AddFiltro(StringBuilder builder, ref int cont) {
            if(this.Condicion == "between" || this.Condicion == "nobetween") {
                if(this.Condicion == "between") {
                    builder.Append($"({this.Id}>=@{cont}&&{this.Id}<=@{cont + 1})");
                } else {
                    builder.Append($"(!({this.Id}>=@{cont}&&{this.Id}<=@{cont + 1}))");
                }
                cont = cont + 2;
                return this.AddRango(this.Criterio1, this.Criterio2);
            } else {
                return this.AddCriterios(builder, ref cont);
            }
        }

        private string[] AddCriterios(StringBuilder builder, ref int cont) {
            builder.Append('(');
            for(var i = 0; i < this.Criterios.Length; i++) {
                this.AddCriterio(builder, cont, (i < (this.Criterios.Length - 1)));
                cont++;
            }
            builder.Append(')');
            return this.Criterios;
        }

        private void AddCriterio(StringBuilder builder, int cont, bool isLastIndex) {
            if(this.Condicion == "contiene") {
                builder.Append($"({this.Id} != null && {this.Id}.Contains(@{cont}))");
                if(isLastIndex) builder.Append("||");
            } else {
                builder.Append($"!({this.Id} != null && {this.Id}.Contains(@{cont}))");
                if(isLastIndex) builder.Append("&&");
            }
        }

        private dynamic[] AddRango(params string[] criterios) {
            var newCriterios = new dynamic[criterios.Length];
            for(var i = 0; i < criterios.Length; i++) {
                newCriterios[i] = this.Id.StartsWith("Fecha") ? DateTime.Parse(criterios[i]) : criterios[i];
            }
            return newCriterios;
        }
    }
}