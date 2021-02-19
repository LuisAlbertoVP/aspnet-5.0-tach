using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class TransaccionValidator : AbstractValidator<Transaccion> {
        public TransaccionValidator() {
            RuleFor(venta => venta.Id).NotNull();
            RuleFor(venta => venta.Fecha).NotNull();
            RuleFor(venta => venta.Cantidad).NotNull();
            RuleFor(venta => venta.Total).NotNull();
            RuleFor(venta => venta.UsuarioIngreso).NotNull().MaximumLength(10);
        }
    }
}