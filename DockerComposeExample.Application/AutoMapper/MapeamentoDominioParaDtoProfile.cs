using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DockerComposeExample.Application.DTO;
using DockerComposeExample.Domain.Models;

namespace DockerComposeExample.Application.AutoMapper
{
    public class MapeamentoDominioParaDtoProfile : Profile
    {
        public MapeamentoDominioParaDtoProfile()
        {
            CreateMap<TrocoItem, TrocoItemDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.IdPagamento, o => o.MapFrom(s => s.IdPagamento))
                .ForMember(d => d.Quantidade, o => o.MapFrom(s => s.Quantidade))
                .ForMember(d => d.TipoItemTroco, o => o.MapFrom(s => s.TipoItemTroco.ToString()))
                .ForMember(d => d.ValorItem, o => o.MapFrom(s => s.ValorItem))
                .ForMember(d => d.ValorTotalItem, o => o.MapFrom(s => s.ValorTotalItem()));

            CreateMap<Pagamento, PagamentoDTO>()
                .ForMember(d => d.IdPagamento, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.DataCadastro, o => o.MapFrom(s => s.DataCadastro))
                .ForMember(d => d.ValorPagamento, o => o.MapFrom(s => s.ValorPagamento))
                .ForMember(d => d.ValorPagoCliente, o => o.MapFrom(s => s.ValorPagoCliente))
                .ForMember(d => d.ValorTroco, o => o.MapFrom(s => s.TrocoItems.Sum(x => x.ValorTotalItem())))
                .ForMember(d => d.TrocoItems, o => o.MapFrom(s => s.TrocoItems));
        }
    }
}
