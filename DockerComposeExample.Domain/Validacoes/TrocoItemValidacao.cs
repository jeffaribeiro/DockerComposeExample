using FluentValidation;
using System;
using DockerComposeExample.Domain.Models;

namespace DockerComposeExample.Domain.Validacoes
{
    public class TrocoItemValidacao : AbstractValidator<TrocoItem>
    {
        public TrocoItemValidacao()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("O campo {PropertyName} não pode ser vazio");

            RuleFor(c => c.ValorItem)
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
        }
    }
}
