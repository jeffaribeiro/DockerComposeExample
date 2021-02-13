using System.Threading.Tasks;
using DockerComposeExample.Domain.Base;
using DockerComposeExample.Domain.Interfaces;
using DockerComposeExample.Domain.Models;
using DockerComposeExample.Domain.Notificacoes;
using DockerComposeExample.Domain.Repository.Dapper;
using DockerComposeExample.Domain.Repository.EFCore;
using DockerComposeExample.Domain.Validacoes;

namespace DockerComposeExample.Domain.Services
{
    public class PagamentoService : ServiceBase, IPagamentoService
    {
        private readonly IPagamentoDapperRepository _pagamentoDapperRepository;
        private readonly ITrocoItemDapperRepository _trocoItemDapperRepository;
        private readonly IPagamentoEFCoreRepository _pagamentoEFCoreRepository;
        private readonly ITrocoItemEFCoreRepository _trocoItemEFCoreRepository;

        public PagamentoService(IPagamentoDapperRepository pagamentoDapperRepository,
                                ITrocoItemDapperRepository trocoItemDapperRepository,
                                IPagamentoEFCoreRepository pagamentoEFCoreRepository,
                                ITrocoItemEFCoreRepository trocoItemEFCoreRepository,
                                INotificador notificador) : base(notificador)
        {
            _pagamentoDapperRepository = pagamentoDapperRepository;
            _trocoItemDapperRepository = trocoItemDapperRepository;
            _pagamentoEFCoreRepository = pagamentoEFCoreRepository;
            _trocoItemEFCoreRepository = trocoItemEFCoreRepository;
        }

        public async Task<Pagamento> ReceberPagamento(decimal valorPagamento, decimal valorPagoCliente)
        {
            var pagamento = new Pagamento(valorPagamento, valorPagoCliente);

            if (!ExecutarValidacao(new PagamentoValidacao(), pagamento)) return null;

            await _pagamentoEFCoreRepository.Adicionar(pagamento);

            foreach (var trocoItem in pagamento.TrocoItems)
            {
                trocoItem.AssociarPagamento(pagamento.Id);

                if (!ExecutarValidacao(new TrocoItemValidacao(), trocoItem)) return null;

                await _trocoItemEFCoreRepository.Adicionar(trocoItem);
            }

            return pagamento;
        }
    }
}
