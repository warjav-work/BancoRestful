using FluentValidation;

namespace Application.Features.Authenticate.Commands.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.Nombre)
          .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
          .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(c => c.Apellido)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(c => c.Email)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de email valida")
               .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(c => c.UserName)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(c => c.Password)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(c => c.ConfirmPassword)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .MaximumLength(15).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.")
               .Equal(c => c.Password).WithMessage("{PropertyName} debe ser igual a Password.");

        }
    }
}
