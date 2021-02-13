using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using DockerComposeExample.Domain.Notificacoes;
using DockerComposeExample.Domain.Repository.Dapper;
using DockerComposeExample.Domain.Repository.EFCore;
using DockerComposeExample.Domain.Services;
using Xunit;

namespace DockerComposeExample.UnitTests.Domain.Services
{
    public class PagamentoServiceTests
    {
        [Theory]
        [InlineData(0.50, 1)]
        [InlineData(0.50, 0.50)]
        public async void PagamentoService_ReceberPagamento_DeveExecutarComSucesso(decimal valorPagamento, decimal valorPagoCliente)
        {
            // Arrange
            var pagamentoDapperRepository = new Mock<IPagamentoDapperRepository>();
            var trocoItemDapperRepository = new Mock<ITrocoItemDapperRepository>();
            var pagamentoEFCoreRepository = new Mock<IPagamentoEFCoreRepository>();
            var trocoItemEFCoreRepository = new Mock<ITrocoItemEFCoreRepository>();
            var notificador = new Mock<INotificador>();

            var pagamentoService = new PagamentoService(pagamentoDapperRepository.Object,
                    trocoItemDapperRepository.Object, pagamentoEFCoreRepository.Object, trocoItemEFCoreRepository.Object, notificador.Object);

            // Act
            var pagamento = await pagamentoService.ReceberPagamento(valorPagamento, valorPagoCliente); 

            // Assert
            Assert.NotNull(pagamento);
            pagamentoEFCoreRepository.Verify(r => r.Adicionar(pagamento), Times.Once);
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(0, 0)]
        public async void PagamentoService_ReceberPagamento_DeveFalhar(decimal valorPagamento, decimal valorPagoCliente)
        {
            // Arrange
            var pagamentoDapperRepository = new Mock<IPagamentoDapperRepository>();
            var trocoItemDapperRepository = new Mock<ITrocoItemDapperRepository>();
            var pagamentoEFCoreRepository = new Mock<IPagamentoEFCoreRepository>();
            var trocoItemEFCoreRepository = new Mock<ITrocoItemEFCoreRepository>();
            var notificador = new Mock<INotificador>();

            var pagamentoService = new PagamentoService(pagamentoDapperRepository.Object,
                    trocoItemDapperRepository.Object, pagamentoEFCoreRepository.Object, trocoItemEFCoreRepository.Object, notificador.Object);

            // Act
            var pagamento = await pagamentoService.ReceberPagamento(valorPagamento, valorPagoCliente);

            // Assert
            Assert.Null(pagamento);
            pagamentoEFCoreRepository.Verify(r => r.Adicionar(pagamento), Times.Never);
        }
    }
}