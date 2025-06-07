using FluentValidation;
using Newtonsoft.Json.Linq;
using PrismaCatalogo.Front.Cliente.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PrismaCatalogo.Validations
{
    public class TamanhoValidator : AbstractValidator<TamanhoViewModel>
    {
        public TamanhoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Informe algum nome para o tamanho!");
        }
    }
}
