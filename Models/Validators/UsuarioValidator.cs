using FluentValidation;

namespace Tach.Models.Validators {
    public class UsuarioValidator : CuentaValidator {
        public UsuarioValidator() : base() {
            RuleFor(usuario => usuario.FechaContratacion).NotNull();
            RuleFor(usuario => usuario.Salario).NotNull();
            RuleFor(usuario => usuario.Roles).Must(roles => roles?.Count > 0)
                .ForEach(roles => roles.Must(rol => !string.IsNullOrEmpty(rol.Id)));
        }
    }
}