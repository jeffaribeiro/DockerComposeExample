using System;
using System.Threading.Tasks;
using DockerComposeExample.Domain.Models;

namespace DockerComposeExample.Domain.Interfaces
{
    public interface IPagamentoService
    {
        Task<Pagamento> ReceberPagamento(decimal valorPagamento, decimal valorPagoPeloCliente);
    }
}
