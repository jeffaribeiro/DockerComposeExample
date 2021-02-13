using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using DockerComposeExample.Application.DTO;
using DockerComposeExample.Application.Interfaces;
using DockerComposeExample.Domain.Notificacoes;

namespace DockerComposeExample.Api.Controllers
{
    [Route("api/[controller]")]
    public class PagamentoController : MainController
    {
        private readonly IPagamentoAppService _pagamentoAppService;

        public PagamentoController(IPagamentoAppService pagamentoAppService, INotificador notificador, IDistributedCache cache) : base(notificador, cache)
        {
            _pagamentoAppService = pagamentoAppService;
        }

        [HttpPost("pagar")]
        public async Task<ActionResult<PagamentoDTO>> Pagar(PagamentoInputDTO pagamentoDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var troco = await _pagamentoAppService.EfetuarPagamento(pagamentoDTO);

            return CustomResponse(troco);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagamentoDTO>>> BuscarTodos()
        {
            IEnumerable<PagamentoDTO> pagamentos = null;
            var pagamentosCacheRedis = await LerCacheRedis("Pagamentos");
            
            if (pagamentosCacheRedis != null)
            {
                pagamentos = JsonConvert.DeserializeObject<IEnumerable<PagamentoDTO>>(pagamentosCacheRedis);
                return CustomResponse(pagamentos); 
            }

            pagamentos = await _pagamentoAppService.BuscarTodos();

            pagamentosCacheRedis = JsonConvert.SerializeObject(pagamentos);

            await ArmazenarCacheRedis("Pagamentos", pagamentosCacheRedis);

            return CustomResponse(pagamentos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PagamentoDTO>> BuscarPorId(Guid id)
        {
            var pagamento = await _pagamentoAppService.BuscarPorId(id);

            if (pagamento == null) return NotFound();

            return CustomResponse(pagamento);
        }
    }
}