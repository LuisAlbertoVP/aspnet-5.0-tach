using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class TransaccionValidator<T> : AbstractValidator<T> where T: Transaccion {
        public TransaccionValidator() {
            RuleFor(transaccion => transaccion.Id).NotNull();
            RuleFor(transaccion => transaccion.Fecha).NotNull();
            RuleFor(transaccion => transaccion.Cantidad).NotNull();
            RuleFor(transaccion => transaccion.Total).NotNull();
        }
    }
}