using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Distributed;
using DockerComposeExample.Api.DTO;
using DockerComposeExample.Domain.Notificacoes;

namespace DockerComposeExample.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly INotificador _notificador;
        protected MainController(INotificador notificador, IDistributedCache cache)
        {
            _cache = cache;
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new ResponseSuccessoDTO
                {
                    Data = result
                });
            }

            return BadRequest(new ResponseErroDTO
            {
                Errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem).ToList()
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected async Task ArmazenarCacheRedis(string chave, string valor)
        {
            DistributedCacheEntryOptions opcoesCache = new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            await _cache.SetStringAsync(chave, valor, opcoesCache);
        }

        protected async Task<string> LerCacheRedis(string chave)
        {
            return await _cache.GetStringAsync(chave);
        }
    }
}
