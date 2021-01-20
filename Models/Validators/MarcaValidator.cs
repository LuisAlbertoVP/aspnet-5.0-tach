using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class MarcaValidator : AbstractValidator<Marca> {
        public MarcaValidator() {
            RuleFor(marca => marca.Id).NotNull();
            RuleFor(marca => marca.Descripcion).NotNull().MaximumLength(50);
        }
    }
}