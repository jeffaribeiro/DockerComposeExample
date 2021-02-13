using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DockerComposeExample.Application.DTO;

namespace DockerComposeExample.Application.Interfaces
{
    public interface IPagamentoAppService
    {
        Task<PagamentoDTO> EfetuarPagamento(PagamentoInputDTO pagamentoDTO);
        Task<PagamentoDTO> BuscarPorId(Guid id);
        Task<IEnumerable<PagamentoDTO>> BuscarTodos();
    }
}
