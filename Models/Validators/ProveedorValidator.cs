using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class ProveedorValidator : AbstractValidator<Proveedor> {
        public ProveedorValidator() {
            RuleFor(proveedor => proveedor.Id).NotNull();
            RuleFor(proveedor => proveedor.Descripcion).NotNull().MaximumLength(50);
            RuleFor(proveedor => proveedor.Telefono).MaximumLength(25);
            RuleFor(proveedor => proveedor.Correo).MaximumLength(320);
        }
    }
}