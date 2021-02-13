using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DockerComposeExample.Api.Controllers;
using DockerComposeExample.Application.DTO;
using DockerComposeExample.Application.Interfaces;
using DockerComposeExample.Domain.Models;
using DockerComposeExample.Domain.Notificacoes;
using Xunit;

namespace DockerComposeExample.UnitTests.Api
{
    public class PagamentoControllerTests
    {
        [Fact]
        public async void ObterPorId_DeveRetornarNotFound()
        {
            // Arrange
            var pagamentoAppService = new Mock<IPagamentoAppService>();
            var notificador = new Mock<INotificador>();
            var cache = new Mock<IDistributedCache>();

            var controller = new PagamentoController(pagamentoAppService.Object, notificador.Object, cache.Object);

            // Act
            var retorno = await controller.BuscarPorId(Guid.NewGuid());

            // Assert
            Assert.IsType<ActionResult<PagamentoDTO>>(retorno);
            Assert.IsType<NotFoundResult>(retorno.Result);
        }

        [Fact]
        public async void ObterPorId_DeveRetornarOK()
        {
            // Arrange
            var pagamentoAppService = new Mock<IPagamentoAppService>();
            var notificador = new Mock<INotificador>();
            var cache = new Mock<IDistributedCache>();
            var pagamentoDtoEsperado = new PagamentoDTO()
            {
                DataCadastro = DateTimeOffset.Now,
                IdPagamento = Guid.NewGuid(),
                ValorPagamento = 1,
                ValorPagoCliente = 1,
                ValorTroco = 0,
                TrocoItems = new List<TrocoItemDTO>()
            };

            pagamentoAppService.Setup(appService => appService.BuscarPorId(pagamentoDtoEsperado.IdPagamento))
                .ReturnsAsync(pagamentoDtoEsperado);

            var controller = new PagamentoController(pagamentoAppService.Object, notificador.Object, cache.Object);

            // Act
            var retorno = await controller.BuscarPorId(pagamentoDtoEsperado.IdPagamento);

            // Assert
            Assert.IsType<ActionResult<PagamentoDTO>>(retorno);
            Assert.IsType<OkObjectResult>(retorno.Result);
        }

        [Fact]
        public async void ObterPorId_DeveRetornarBadRequest()
        {
            // Arrange
            var pagamentoAppService = new Mock<IPagamentoAppService>();
            var notificador = new Mock<INotificador>();
            var cache = new Mock<IDistributedCache>();
            var pagamentoDtoEsperado = new PagamentoDTO()
            {
                DataCadastro = DateTimeOffset.Now,
                IdPagamento = Guid.NewGuid(),
                ValorPagamento = 1,
                ValorPagoCliente = 1,
                ValorTroco = 0,
                TrocoItems = new List<TrocoItemDTO>()
            };

            pagamentoAppService.Setup(appService => appService.BuscarPorId(pagamentoDtoEsperado.IdPagamento))
                .ReturnsAsync(pagamentoDtoEsperado);

            notificador.Setup(n => n.TemNotificacao()).Returns(true);
            notificador.Setup(n => n.ObterNotificacoes()).Returns(new List<Notificacao>() { new Notificacao("erro 1"), new Notificacao("erro 2") });

            var controller = new PagamentoController(pagamentoAppService.Object, notificador.Object, cache.Object);

            // Act
            var retorno = await controller.BuscarPorId(pagamentoDtoEsperado.IdPagamento);

            // Assert
            Assert.IsType<ActionResult<PagamentoDTO>>(retorno);
            Assert.IsType<BadRequestObjectResult>(retorno.Result);
        }

        [Fact]
        public async void Pagar_DeveRetornarOk()
        {
            // Arrange
            var pagamentoAppService = new Mock<IPagamentoAppService>();
            var notificador = new Mock<INotificador>();
            var cache = new Mock<IDistributedCache>();

            notificador.Setup(n => n.TemNotificacao()).Returns(false);

            var controller = new PagamentoController(pagamentoAppService.Object, notificador.Object, cache.Object);

            // Act
            var retorno = await controller.Pagar(new PagamentoInputDTO() { ValorPagoCliente = 2, ValorPagamento = 2 });

            // Assert
            Assert.IsType<ActionResult<PagamentoDTO>>(retorno);
            Assert.IsType<OkObjectResult>(retorno.Result);
        }

        [Fact]
        public async void Pagar_DeveRetornarModelStateInvalido()
        {
            // Arrange
            var pagamentoAppService = new Mock<IPagamentoAppService>();
            var notificador = new Mock<INotificador>();
            var cache = new Mock<IDistributedCache>();

            notificador.Setup(n => n.TemNotificacao()).Returns(true);
            notificador.Setup(n => n.ObterNotificacoes()).Returns(new List<Notificacao>() { new Notificacao("Erro 1") });

            var controller = new PagamentoController(pagamentoAppService.Object, notificador.Object, cache.Object);
            controller.ModelState.AddModelError("1", "Erro 1");

            // Act
            var retorno = await controller.Pagar(new PagamentoInputDTO());

            // Assert
            Assert.IsType<ActionResult<PagamentoDTO>>(retorno);
            Assert.IsType<BadRequestObjectResult>(retorno.Result);
        }

        private IEnumerable<PagamentoDTO> ObterPagamentos()
        {
            var pagamentos = new List<PagamentoDTO>();

            var pagamentoId01 = Guid.NewGuid();
            var pagamentoId02 = Guid.NewGuid();

            pagamentos.Add(new PagamentoDTO()
            {
                DataCadastro = DateTimeOffset.Now,
                IdPagamento = pagamentoId01,
                ValorPagamento = 20,
                ValorPagoCliente = 50,
                ValorTroco = 30,
                TrocoItems = new List<TrocoItemDTO>() {
                      new TrocoItemDTO()
                      {
                          Id = Guid.NewGuid(),
                          IdPagamento = pagamentoId01,
                          Quantidade = 1,
                          TipoItemTroco = "Nota",
                          ValorItem = 20,
                          ValorTotalItem = 20
                      },
                      new TrocoItemDTO()
                      {
                          Id = Guid.NewGuid(),
                          IdPagamento = pagamentoId01,
                          Quantidade = 1,
                          TipoItemTroco = "Nota",
                          ValorItem = 10,
                          ValorTotalItem = 10
                      }
                  }
            });

            pagamentos.Add(new PagamentoDTO()
            {
                DataCadastro = DateTimeOffset.Now,
                IdPagamento = pagamentoId02,
                ValorPagamento = 30,
                ValorPagoCliente = 50,
                ValorTroco = 20,
                TrocoItems = new List<TrocoItemDTO>() {
                      new TrocoItemDTO()
                      {
                          Id = Guid.NewGuid(),
                          IdPagamento = pagamentoId02,
                          Quantidade = 1,
                          TipoItemTroco = "Nota",
                          ValorItem = 20,
                          ValorTotalItem = 20
                      }
                  }
            });

            return pagamentos;
        }
    }
}
