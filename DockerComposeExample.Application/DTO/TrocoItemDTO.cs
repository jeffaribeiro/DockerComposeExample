using System;

namespace DockerComposeExample.Application.DTO
{
    public class TrocoItemDTO
    {
        public Guid Id { get; set; }
        public Guid IdPagamento { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorItem { get; set; }
        public string TipoItemTroco { get; set; }
        public decimal ValorTotalItem { get; set; }
    }
}
