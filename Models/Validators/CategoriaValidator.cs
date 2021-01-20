using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class CategoriaValidator : AbstractValidator<Categoria> {
        public CategoriaValidator() {
            RuleFor(categoria => categoria.Id).NotNull();
            RuleFor(categoria => categoria.Descripcion).NotNull().MaximumLength(50);
        }
    }
}