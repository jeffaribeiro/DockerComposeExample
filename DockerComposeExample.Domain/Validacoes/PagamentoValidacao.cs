using FluentValidation;
using System;
using DockerComposeExample.Domain.Models;

namespace DockerComposeExample.Domain.Validacoes
{
    public class PagamentoValidacao : AbstractValidator<Pagamento>
    {
        public PagamentoValidacao()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("O campo {PropertyName} não pode ser vazio");

            RuleFor(c => c.ValorPagamento)
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");

            RuleFor(c => c.ValorPagoCliente)
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");

            RuleFor(c => c.ValorPagoCliente)
                .GreaterThanOrEqualTo(c => c.ValorPagamento).WithMessage("O campo {PropertyName} precisa ser maior ou igual a {ComparisonValue}");

            RuleFor(c => c.TrocoItems)
                .NotNull().WithMessage("O valor do troco não pode ser calculado"); 
        }
    }
}
