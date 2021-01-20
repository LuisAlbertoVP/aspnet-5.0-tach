using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class RolValidator : AbstractValidator<Rol> {
        public RolValidator() {
            RuleFor(rol => rol.Id).NotNull();
            RuleFor(rol => rol.Descripcion).NotNull().MaximumLength(50);
        }
    }
}