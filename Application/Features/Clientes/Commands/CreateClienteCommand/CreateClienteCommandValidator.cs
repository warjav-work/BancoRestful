using FluentValidation;

namespace Application.Features.Clientes.Commands.CreateClienteCommand
{
    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteCommandValidator()
        {
            RuleFor(c => c.Nombre)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(c => c.Apellido)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(c => c.FechaNacimiento)
                .NotEmpty().WithMessage("Fecha de nacimiento no puede ser vacio.");

            RuleFor(c => c.Telefono)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .Matches(@"^\d{4}-\d{4}$").WithMessage("{PropertyName} debe cumplir el formato 0000-0000")
                .MaximumLength(9).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(c => c.Email)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de email valida")
               .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(c => c.Direccion)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(120).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");
        }
    }
}
