using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class CuentaValidator : PersonValidator<Usuario> {
        public CuentaValidator() : base() {
            RuleFor(usuario => usuario.NombreUsuario).NotNull().MaximumLength(10);
        }
    }
}