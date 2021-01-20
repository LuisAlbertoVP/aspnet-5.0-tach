using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class CuentaValidator : AbstractValidator<Usuario> {
        public CuentaValidator() {
            RuleFor(usuario => usuario.Id).NotNull();
            RuleFor(usuario => usuario.NombreUsuario).NotNull().MaximumLength(10);
            RuleFor(usuario => usuario.Nombres).NotNull().MaximumLength(50);
            RuleFor(usuario => usuario.Cedula).NotNull().MaximumLength(10);
            RuleFor(usuario => usuario.Direccion).NotNull();
            RuleFor(usuario => usuario.Telefono).NotNull().MaximumLength(25);
            RuleFor(usuario => usuario.Celular).NotNull().MaximumLength(25);
            RuleFor(usuario => usuario.FechaNacimiento).NotNull();
            RuleFor(usuario => usuario.Correo).NotNull().MaximumLength(320).EmailAddress();
        }
    }
}