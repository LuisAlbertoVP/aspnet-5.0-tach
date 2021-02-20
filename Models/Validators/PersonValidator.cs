using FluentValidation;
using Tach.Models.Entities;

namespace Tach.Models.Validators {
    public class PersonValidator<T> : AbstractValidator<T> where T: Persona {
        public PersonValidator() {
            RuleFor(persona => persona.Id).NotNull();
            RuleFor(persona => persona.Nombres).NotNull().MaximumLength(50);
            RuleFor(persona => persona.Cedula).NotNull().MaximumLength(10);
            RuleFor(persona => persona.Direccion).NotNull();
            RuleFor(persona => persona.Telefono).NotNull().MaximumLength(25);
            RuleFor(persona => persona.Celular).NotNull().MaximumLength(25);
            RuleFor(persona => persona.FechaNacimiento).NotNull();
            RuleFor(persona => persona.Correo).NotNull().MaximumLength(320).EmailAddress();
        }
    }
}