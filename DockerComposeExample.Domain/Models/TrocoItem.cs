using System;
using DockerComposeExample.Domain.Base;

namespace DockerComposeExample.Domain.Models
{
    public class TrocoItem : EntidadeBase
    {
        public Guid IdPagamento { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorItem { get; private set; }
        public TipoItemTroco TipoItemTroco { get; private set; }

        public Pagamento Pagamento { get; private set; }

        protected TrocoItem() { }

        public TrocoItem(decimal valorItem, int quantidade)
        {
            Id = Guid.NewGuid();
            TipoItemTroco = valorItem > 1 ? TipoItemTroco.Nota : TipoItemTroco.Moeda;
            ValorItem = valorItem;
            Quantidade = quantidade;
        }

        public void AssociarPagamento(Guid idPagamento)
        {
            IdPagamento = idPagamento;
        }

        public decimal ValorTotalItem()
        {
            return Quantidade * ValorItem;
        }
    }
}
