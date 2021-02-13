using System;
using System.Collections.Generic;
using System.Linq;
using DockerComposeExample.Domain.Base;

namespace DockerComposeExample.Domain.Models
{
    public class Pagamento : EntidadeBase
    {
        public decimal ValorPagamento { get; private set; }
        public decimal ValorPagoCliente { get; private set; }
        public DateTimeOffset DataCadastro { get; private set; }
        public IList<TrocoItem> TrocoItems { get; private set; }

        protected Pagamento() {
            TrocoItems = new List<TrocoItem>();
        }

        public Pagamento(decimal valorPagamento, decimal valorPagoCliente)
        {
            Id = Guid.NewGuid();
            ValorPagamento = valorPagamento;
            ValorPagoCliente = valorPagoCliente;
            TrocoItems = new List<TrocoItem>();

            this.CalcularTrocoItens();
        }

        private void CalcularTrocoItens()
        {
            var valoresComposicaoTroco = new decimal[] { 100, 50, 20, 10, 0.50M, 0.10M, 0.05M, 0.01M };
            var valorRestanteCalculoTroco = this.ValorPagoCliente - this.ValorPagamento;

            foreach (var valorItemTroco in valoresComposicaoTroco)
            {
                var quantidadeItemTroco = Convert.ToInt32(Math.Truncate(valorRestanteCalculoTroco / valorItemTroco));

                if (quantidadeItemTroco > 0)
                    TrocoItems.Add(new TrocoItem(valorItemTroco, quantidadeItemTroco));
                
                valorRestanteCalculoTroco = valorRestanteCalculoTroco - (quantidadeItemTroco * valorItemTroco);
            }
        }

        public decimal ValorTroco() => TrocoItems.Sum(x => x.ValorTotalItem());
    }
}
