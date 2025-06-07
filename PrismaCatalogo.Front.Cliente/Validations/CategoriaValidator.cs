﻿using FluentValidation;
using PrismaCatalogo.Front.Cliente.Models;

namespace PrismaCatalogo.Validations
{
    public class CategoriaValidator : AbstractValidator<CategoriaViewModel>
    {
        public CategoriaValidator() 
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("Informe algum nome para a categoria!");
        }
    }
}
