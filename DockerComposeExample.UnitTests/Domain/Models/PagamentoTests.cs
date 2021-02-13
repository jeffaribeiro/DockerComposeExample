using System;
using DockerComposeExample.Domain.Models;
using Xunit;

namespace DockerComposeExample.UnitTests.Domain.Models
{
    public class PagamentoTests
    {
        [Fact]
        public void Pagamento_Id_NaoDeveSerUmGuidVazio()
        {
            var pagamento = new Pagamento(1, 10);
            Assert.NotEqual(Guid.Empty, pagamento.Id);
        }

        [Theory]
        [InlineData(0.5, 0.5)]
        [InlineData(0.5, 0.6)]
        public void Pagamento_TrocoItems_NaoDeveSerNulo(decimal valorPagamento, decimal valorPagoCliente)
        {
            var pagamento = new Pagamento(valorPagamento, valorPagoCliente);
            Assert.NotNull(pagamento.TrocoItems);
        }

        [Theory]
        [InlineData(0.01, 0.01)]
        public void Pagamento_ValorPagamento_DeveSerMaiorQueZero(decimal valorPagamento, decimal valorPagoCliente)
        {
            var pagamento = new Pagamento(valorPagamento, valorPagoCliente);
            Assert.True(pagamento.ValorPagamento > 0);
        }

        [Theory]
        [InlineData(0.01, 0.01)]
        public void Pagamento_ValorPagoCliente_DeveSerMaiorQueZero(decimal valorPagamento, decimal valorPagoCliente)
        {
            var pagamento = new Pagamento(valorPagamento, valorPagoCliente);
            Assert.True(pagamento.ValorPagoCliente > 0);
        }

        [Theory]
        [InlineData(0.5, 0.5)]
        [InlineData(0.5, 0.6)]
        public void Pagamento_ValorPagoCliente_DeveSerMaiorOuIgual_ValorPagamento(decimal valorPagamento, decimal valorPagoCliente)
        {
            var pagamento = new Pagamento(valorPagamento, valorPagoCliente);
            Assert.True(pagamento.ValorPagoCliente >= pagamento.ValorPagamento);
        }

        [Theory]
        [InlineData(0.5, 0.6)]
        public void Pagamento_TrocoItemsTemPeloMenosUmItem_E_ValorTrocoMaiorQueZero(decimal valorPagamento, decimal valorPagoCliente)
        {
            var pagamento = new Pagamento(valorPagamento, valorPagoCliente);
            Assert.True(pagamento.TrocoItems.Count > 0);
            Assert.True(pagamento.ValorTroco() > 0);
        }

        [Theory]
        [InlineData(0.5, 0.5)]
        public void Pagamento_TrocoItemsSemItens_E_ValorTrocoMaiorIgualAZero(decimal valorPagamento, decimal valorPagoCliente)
        {
            var pagamento = new Pagamento(valorPagamento, valorPagoCliente);
            Assert.True(pagamento.TrocoItems.Count == 0);
            Assert.True(pagamento.ValorTroco() == 0);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(10, 100)]
        public void Pagamento_ValorCalculoTrocoItems_E_ValorTroco_DevemSerIguais(decimal valorPagamento, decimal valorPagoCliente)
        {
            var pagamento = new Pagamento(valorPagamento, valorPagoCliente);

            var valorCalculoTrocoItems = pagamento.ValorPagoCliente - pagamento.ValorPagamento;
            var valorTroco = pagamento.ValorTroco(); 

            Assert.Equal(valorCalculoTrocoItems, valorTroco);
        }

    }
}
