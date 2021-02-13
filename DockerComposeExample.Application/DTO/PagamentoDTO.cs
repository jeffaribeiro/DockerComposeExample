using System;
using System.Collections.Generic;

namespace DockerComposeExample.Application.DTO
{
    public class PagamentoDTO
    {
        public Guid IdPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
        public decimal ValorPagoCliente { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public decimal ValorTroco { get; set; }
        public IList<TrocoItemDTO> TrocoItems { get; set; }
    }
}
