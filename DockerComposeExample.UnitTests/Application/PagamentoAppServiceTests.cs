using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using DockerComposeExample.Application.AutoMapper;
using DockerComposeExample.Application.DTO;
using DockerComposeExample.Application.Services;
using DockerComposeExample.Domain.Interfaces;
using DockerComposeExample.Domain.Models;
using DockerComposeExample.Domain.Notificacoes;
using DockerComposeExample.Domain.Repository;
using DockerComposeExample.Domain.Repository.Dapper;
using Xunit;

namespace DockerComposeExample.UnitTests.Application
{
    public class PagamentoAppServiceTests
    {
        private readonly MapperConfiguration configuration;

        public PagamentoAppServiceTests()
        {
            var profile = new MapeamentoDominioParaDtoProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
        }

        [Fact]
        public async void BuscarTodos_DeveRetornarItens()
        {
            // Arrange
            var pagamentoService = new Mock<IPagamentoService>();
            var pagamentoDapperRepository = new Mock<IPagamentoDapperRepository>();
            var mapper = new Mapper(configuration);
            var unitOfWork = new Mock<IUnitOfWork>();
            var notificador = new Mock<INotificador>();
            var pagamentosEsperados = ObterPagamentos();

            pagamentoDapperRepository.Setup(repository => repository.BuscarTodos())
                .ReturnsAsync(pagamentosEsperados);

            var pagamentoAppService = new PagamentoAppService(pagamentoService.Object, pagamentoDapperRepository.Object,
                mapper, unitOfWork.Object, notificador.Object);

            // Act
            var listaPagamentoDto = await pagamentoAppService.BuscarTodos();

            // Assert
            Assert.True(listaPagamentoDto.Any());
        }

        [Fact]
        public async void BuscarPorId_DeveRetornarItem()
        {
            // Arrange
            var pagamentoService = new Mock<IPagamentoService>();
            var pagamentoDapperRepository = new Mock<IPagamentoDapperRepository>();
            var mapper = new Mapper(configuration);
            var unitOfWork = new Mock<IUnitOfWork>();
            var notificador = new Mock<INotificador>();
            var pagamentoEsperado = new Pagamento(2, 5);

            pagamentoDapperRepository.Setup(repository => repository.BuscarPorId(pagamentoEsperado.Id))
                .ReturnsAsync(pagamentoEsperado);

            var pagamentoAppService = new PagamentoAppService(pagamentoService.Object, pagamentoDapperRepository.Object,
                mapper, unitOfWork.Object, notificador.Object);

            // Act
            var pagamentoDto = await pagamentoAppService.BuscarPorId(pagamentoEsperado.Id);

            // Assert
            Assert.NotNull(pagamentoDto);
        }

        [Fact]
        public async void EfetuarPagamento_DeveRetornarItem()
        {
            // Arrange
            var pagamentoService = new Mock<IPagamentoService>();
            var pagamentoDapperRepository = new Mock<IPagamentoDapperRepository>();
            var mapper = new Mapper(configuration);
            var unitOfWork = new Mock<IUnitOfWork>();
            var notificador = new Mock<INotificador>();
            var pagamentoEsperado = new Pagamento(2, 5);
            var pagamentoInputDto = new PagamentoInputDTO
            {
                ValorPagamento = 2,
                ValorPagoCliente = 5
            };

            pagamentoService.Setup(service => service.ReceberPagamento(2, 5))
                .ReturnsAsync(pagamentoEsperado);

            notificador.Setup(n => n.TemNotificacao()).Returns(false); 

            var pagamentoAppService = new PagamentoAppService(pagamentoService.Object, pagamentoDapperRepository.Object,
                mapper, unitOfWork.Object, notificador.Object);

            // Act
            var pagamentoDto = await pagamentoAppService.EfetuarPagamento(pagamentoInputDto);

            // Assert
            Assert.NotNull(pagamentoDto);
        }

        [Fact]
        public async void EfetuarPagamento_DeveRetornarNulo()
        {
            // Arrange
            var pagamentoService = new Mock<IPagamentoService>();
            var pagamentoDapperRepository = new Mock<IPagamentoDapperRepository>();
            var mapper = new Mapper(configuration);
            var unitOfWork = new Mock<IUnitOfWork>();
            var notificador = new Mock<INotificador>();
            var pagamentoEsperado = new Pagamento(2, 5);
            var pagamentoInputDto = new PagamentoInputDTO
            {
                ValorPagamento = 2,
                ValorPagoCliente = 5
            };

            pagamentoService.Setup(service => service.ReceberPagamento(2, 5))
                .ReturnsAsync(pagamentoEsperado);

            notificador.Setup(n => n.TemNotificacao()).Returns(true);

            var pagamentoAppService = new PagamentoAppService(pagamentoService.Object, pagamentoDapperRepository.Object,
                mapper, unitOfWork.Object, notificador.Object);

            // Act
            var pagamentoDto = await pagamentoAppService.EfetuarPagamento(pagamentoInputDto);

            // Assert
            Assert.Null(pagamentoDto);
        }

        private IEnumerable<Pagamento> ObterPagamentos()
        {
            var pagamentos = new List<Pagamento>();

            pagamentos.Add(new Pagamento(20, 50));
            pagamentos.Add(new Pagamento(30, 50));
            pagamentos.Add(new Pagamento(30, 30));
            pagamentos.Add(new Pagamento(27.55M, 50));

            return pagamentos;
        }
    }
}
