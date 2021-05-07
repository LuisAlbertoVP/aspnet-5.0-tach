using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class CompraValidator : TransaccionValidator<Compra> {
        public CompraValidator() : base() {
            RuleFor(compra => compra.TipoDocumento).NotNull().MaximumLength(50);
            RuleFor(compra => compra.Numero).NotNull().MaximumLength(25);
            RuleFor(compra => compra.CompraDetalle)
                .Must(detalles => detalles?.Count > 0)
                .ForEach(detalles => detalles.Must(detalle => {
                    return !string.IsNullOrEmpty(detalle.RepuestoId);
                }));
        }
    }
}