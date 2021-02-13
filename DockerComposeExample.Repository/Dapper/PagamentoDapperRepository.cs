using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerComposeExample.Domain.Models;
using DockerComposeExample.Domain.Repository.Dapper;
using DockerComposeExample.Repository.Base;

namespace DockerComposeExample.Repository.Dapper
{
    public class PagamentoDapperRepository : DapperRepositoryBase<Pagamento>, IPagamentoDapperRepository
    {
        public PagamentoDapperRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override async Task<Pagamento> BuscarPorId(Guid id)
        {
            var pagamentoDictionary = new Dictionary<Guid, Pagamento>();

            var resultado = await conn.QueryAsync<Pagamento, TrocoItem, Pagamento>(
                    "SELECT p.Id, " +
                           "p.ValorPagamento, " +
                           "p.ValorPagoCliente, " +
                           "p.DataCadastro, " +
                           "ti.Id as TrocoItemId, " +
                           "ti.Quantidade, " +
                           "ti.ValorItem, " +
                           "ti.TipoItemTroco " +
                    "FROM Pagamento p " +
                    "LEFT JOIN TrocoItem ti ON ti.IdPagamento = p.Id " +
                    "WHERE p.Id = @id " +
                    "ORDER BY p.DataCadastro desc",
                    (pagamento, trocoItem) =>
                    {
                        if (!pagamentoDictionary.TryGetValue(pagamento.Id, out var pagamentoEntry))
                        {
                            pagamentoEntry = pagamento;
                            pagamentoDictionary.Add(pagamentoEntry.Id, pagamentoEntry);
                        }
                        if (trocoItem.ValorItem > 0)
                        {
                            trocoItem.AssociarPagamento(pagamentoEntry.Id);
                            pagamentoEntry.TrocoItems.Add(trocoItem);
                        }
                        return pagamentoEntry;
                    },
                    new { id = id },
                    splitOn: "TrocoItemId");

            var pagamento = resultado.Distinct().FirstOrDefault();

            return pagamento;
        }

        public override async Task<IEnumerable<Pagamento>> BuscarTodos()
        {
            var pagamentoDictionary = new Dictionary<Guid, Pagamento>();

            var resultado = await conn.QueryAsync<Pagamento, TrocoItem, Pagamento>(
                    "SELECT p.Id, " +
                           "p.ValorPagamento, " +
                           "p.ValorPagoCliente, " +
                           "p.DataCadastro, " +
                           "ti.Id as TrocoItemId, " +
                           "ti.Quantidade, " +
                           "ti.ValorItem, " +
                           "ti.TipoItemTroco " +
                    "FROM Pagamento p " +
                    "LEFT JOIN TrocoItem ti ON ti.IdPagamento = p.Id " +
                    "ORDER BY p.DataCadastro desc",
                    (pagamento, trocoItem) =>
                    {
                        if (!pagamentoDictionary.TryGetValue(pagamento.Id, out var pagamentoEntry))
                        {
                            pagamentoEntry = pagamento;
                            pagamentoDictionary.Add(pagamentoEntry.Id, pagamentoEntry);
                        }
                        if (trocoItem.ValorItem > 0)
                        {
                            trocoItem.AssociarPagamento(pagamentoEntry.Id);
                            pagamentoEntry.TrocoItems.Add(trocoItem);
                        }
                        return pagamentoEntry;
                    },
                    splitOn: "TrocoItemId");

            var pagamentos = resultado.Distinct().ToList();

            return pagamentos;
        }
    }
}
