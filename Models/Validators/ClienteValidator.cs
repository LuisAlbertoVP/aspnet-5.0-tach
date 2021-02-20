using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class ClienteValidator : PersonValidator<Cliente> {
        public ClienteValidator() : base() {
            RuleFor(cliente => cliente.TipoCliente).NotNull().MaximumLength(25);
        }
    }
}