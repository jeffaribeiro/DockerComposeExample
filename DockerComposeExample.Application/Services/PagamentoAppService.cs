using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DockerComposeExample.Application.Base;
using DockerComposeExample.Application.DTO;
using DockerComposeExample.Application.Interfaces;
using DockerComposeExample.Domain.Interfaces;
using DockerComposeExample.Domain.Notificacoes;
using DockerComposeExample.Domain.Repository;
using DockerComposeExample.Domain.Repository.Dapper;

namespace DockerComposeExample.Application.Services
{
    public class PagamentoAppService : AppServiceBase, IPagamentoAppService
    {
        private readonly IPagamentoService _pagamentoService;
        private readonly IPagamentoDapperRepository _pagamentoDapperRepository;
        private readonly IMapper _mapper;

        public PagamentoAppService(IPagamentoService pagamentoService, IPagamentoDapperRepository pagamentoDapperRepository, IMapper mapper, IUnitOfWork unitOfWork, INotificador notificador) : base(unitOfWork, notificador)
        {
            _pagamentoService = pagamentoService;
            _pagamentoDapperRepository = pagamentoDapperRepository;
            _mapper = mapper;
        }

        public async Task<PagamentoDTO> EfetuarPagamento(PagamentoInputDTO pagamentoInputDTO)
        {
            var pagamentoRecebido = await _pagamentoService.ReceberPagamento(pagamentoInputDTO.ValorPagamento, pagamentoInputDTO.ValorPagoCliente);

            if (!Commit())
                return null;
            
            var retorno = _mapper.Map<PagamentoDTO>(pagamentoRecebido);

            return retorno;
        }

        public async Task<PagamentoDTO> BuscarPorId(Guid id)
        {
            var pagamento = await _pagamentoDapperRepository.BuscarPorId(id);

            var retorno = _mapper.Map<PagamentoDTO>(pagamento);

            return retorno;
        }

        public async Task<IEnumerable<PagamentoDTO>> BuscarTodos()
        {
            var pagamentos = await _pagamentoDapperRepository.BuscarTodos();

            var retorno = _mapper.Map<IEnumerable<PagamentoDTO>>(pagamentos);

            return retorno;
        }
    }
}