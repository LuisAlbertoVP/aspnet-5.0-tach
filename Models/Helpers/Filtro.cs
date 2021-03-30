using System;
using System.Text;

namespace Tach.Models.Helpers {
    public class Filtro {
        
        public string Id { get; set; }

        public string Criterio1 { get; set; }

        public string Criterio2 { get; set; }

        public string[] Criterios { get; set; }

        public string Operador { get; set; }

        public dynamic[] AddFiltro(StringBuilder builder, ref int cont) {
            if(this.Operador == "between") {
                builder.Append($"({this.Id}>=@{cont}&&{this.Id}<=@{cont + 1})");
                cont = cont + 2;
                return this.VerifyDate(this.Criterio1, this.Criterio2);
            } else {
                return this.AddCriterios(builder, ref cont);
            }
        }

        private string[] AddCriterios(StringBuilder builder, ref int cont) {
            builder.Append('(');
            for(var i = 0; i < this.Criterios.Length; i++) {
                this.VerifyLastIndex(i, builder, this.AppendQuery(builder, cont));
                cont++;
            }
            builder.Append(')');
            return this.Criterios;
        }

        private string AppendQuery(StringBuilder builder, int cont) {
            if(this.Operador == "contiene") {
                builder.Append($"{this.Id}.Contains(@{cont})");
                return "||";
            } else {
                builder.Append($"!{this.Id}.Contains(@{cont})");
                return "&&";
            }
        }

        private dynamic[] VerifyDate(params string[] criterios) {
            var newCriterios = new dynamic[criterios.Length];
            for(var i = 0; i < criterios.Length; i++) {
                newCriterios[i] = this.Id.StartsWith("Fecha") ? DateTime.Parse(criterios[i]) : criterios[i];
            }
            return newCriterios;
        }

        private void VerifyLastIndex(int index, StringBuilder builder, string operador) {
            if(index < (this.Criterios.Length - 1)) {
                builder.Append(operador);
            }
        }
    }
}