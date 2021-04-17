using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class VentaValidator : TransaccionValidator<Venta> {
        public VentaValidator() : base() {
            RuleFor(venta => venta.Direccion).NotNull();
            RuleFor(venta => venta.VentaDetalle)
                .Must(detalles => detalles?.Count > 0)
                .ForEach(detalles => detalles.Must(detalle => {
                    return !string.IsNullOrEmpty(detalle.RepuestoId) && detalle.Cantidad > 0;
                }));
        }
    }
}