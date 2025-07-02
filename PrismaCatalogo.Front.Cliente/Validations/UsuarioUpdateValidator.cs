using FluentValidation;
using PrismaCatalogo.Front.Cliente.Models;

namespace PrismaCatalogo.Validations
{
    public class UsuarioUpdateValidator : AbstractValidator<UsuarioViewModel>
    {
        public UsuarioUpdateValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Informe um nome para o usuario!");

            RuleFor(x => x.NomeUsuario)
                .NotEmpty()
                .WithMessage("Informe um nome para o usuario!");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("Informe uma senha para o usuario!");
        }
    }
}
