using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class RepuestoValidator : AbstractValidator<Repuesto> {
        public RepuestoValidator() {
            RuleFor(repuesto => repuesto.Id).NotNull();
            RuleFor(repuesto => repuesto.Codigo).NotNull().MaximumLength(50);
            RuleFor(repuesto => repuesto.Marca.Id).NotNull().When(repuesto => repuesto.Marca != null);
            RuleFor(repuesto => repuesto.Categoria.Id).NotNull().When(repuesto => repuesto.Categoria != null);
            RuleFor(repuesto => repuesto.Modelo).NotNull().MaximumLength(50);
            RuleFor(repuesto => repuesto.Epoca).MaximumLength(50);
            RuleFor(repuesto => repuesto.SubMarca).MaximumLength(50);
            RuleFor(repuesto => repuesto.Stock).NotNull();
            RuleFor(repuesto => repuesto.Precio).NotNull();
        }
    }
}