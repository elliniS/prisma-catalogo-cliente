using FluentValidation;
using PrismaCatalogo.Validations;
using PrismaCatalogo.Front.Cliente.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PrismaCatalogo.Validations
{
    public class ProdutoValidator : AbstractValidator<ProdutoViewModel>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Informe algum nome para o produto!");
        }
    }
}
