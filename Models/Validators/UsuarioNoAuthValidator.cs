using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class UsuarioNoAuthValidator : AbstractValidator<Usuario> {
        public UsuarioNoAuthValidator() {
            RuleFor(usuario => usuario.NombreUsuario).NotNull();
            RuleFor(usuario => usuario.Clave).NotNull();
        }
    }
}