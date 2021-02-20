using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class CompraValidator : TransaccionValidator<Compra> {
        public CompraValidator() : base() {
            RuleFor(compra => compra.CompraDetalle)
                .Must(detalles => detalles?.Count > 0)
                .ForEach(detalles => detalles.Must(detalle => {
                    return !string.IsNullOrEmpty(detalle.RepuestoId) && detalle.Cantidad > 0;
                }));
        }
    }
}