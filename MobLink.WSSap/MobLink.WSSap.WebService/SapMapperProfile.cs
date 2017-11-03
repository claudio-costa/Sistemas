using AutoMapper;

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
namespace MobLink.WSSap.WebService
{
    public class SapMapperProfile : Profile
    {
        public static void Iniciar()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Repositorio.si_ordem_interna_requestService.dt_titulo_ord_int_fb70, Dominio.OrdemInternaFB70>()

                .ForMember(y => y.codigo_banco, opt => { opt.MapFrom(x => x.HBKID); })
                .ForMember(y => y.codigo_cliente, opt => { opt.MapFrom(x => x.KUNNR); })
                .ForMember(y => y.codigo_empresa, opt => { opt.MapFrom(x => x.BUKRS); })
                .ForMember(y => y.condicao_pagamento, opt => { opt.MapFrom(x => x.ZTERM); })
                .ForMember(y => y.data_fatura, opt => { opt.MapFrom(x => x.BLDAT); })
                .ForMember(y => y.data_pagamento, opt => { opt.MapFrom(x => x.BLDAT_PAG); })
                .ForMember(y => y.forma_pagamento, opt => { opt.MapFrom(x => x.ZLSCH); })
                .ForMember(y => y.identificacao_leilao_patio_lote, opt => { opt.MapFrom(x => x.XREF1_HD); })
                .ForMember(y => y.montante_bruto, opt => { opt.MapFrom(x => x.WRBTR); })
                .ForMember(y => y.numero_boleto_pagamento, opt => { opt.MapFrom(x => x.XREF2_HD); })
                .ForMember(y => y.numero_ordem_interna, opt => { opt.MapFrom(x => x.BKTXT); })
                .ForMember(y => y.opcao_valor_desconto, opt => { opt.MapFrom(x => x.WRBTR_DESCSpecified); })
                .ForMember(y => y.opcao_valor_pagamento_maior, opt => { opt.MapFrom(x => x.WRBTR_PAG_MAIORSpecified); })
                .ForMember(y => y.opcao_valor_tarifa_bancaria, opt => { opt.MapFrom(x => x.WRBTR_TARIFSpecified); })
                .ForMember(y => y.transacao, opt => { opt.MapFrom(x => x.IDTRANSACAO); })
                .ForMember(y => y.valor_comissao, opt => { opt.MapFrom(x => x.WRBTR_COM); })
                .ForMember(y => y.valor_desconto, opt => { opt.MapFrom(x => x.WRBTR_DESC); })
                .ForMember(y => y.valor_lote, opt => { opt.MapFrom(x => x.WRBTR_LOTE); })
                .ForMember(y => y.valor_pagamento_maior, opt => { opt.MapFrom(x => x.WRBTR_PAG_MAIOR); })
                .ForMember(y => y.valor_tarifa_bancaria, opt => { opt.MapFrom(x => x.WRBTR_TARIF); })
                .ForMember(y => y.valor_taxa_administrativa, opt => { opt.MapFrom(x => x.WRBTR_TAXA); })
                .ForMember(y => y.valor_total_pago, opt => { opt.MapFrom(x => x.WRBTR_PAG); })
                ;

                cfg.CreateMap<Dominio.OrdemInternaFB70, Repositorio.si_ordem_interna_requestService.dt_titulo_ord_int_fb70>()

                .ForMember(y => y.HBKID, opt => { opt.MapFrom(x => x.codigo_banco); })
                .ForMember(y => y.KUNNR, opt => { opt.MapFrom(x => x.codigo_cliente); })
                .ForMember(y => y.BUKRS, opt => { opt.MapFrom(x => x.codigo_empresa); })
                .ForMember(y => y.ZTERM, opt => { opt.MapFrom(x => x.condicao_pagamento); })
                .ForMember(y => y.BLDAT, opt => { opt.MapFrom(x => x.data_fatura); })
                .ForMember(y => y.BLDAT_PAG, opt => { opt.MapFrom(x => x.data_pagamento); })
                .ForMember(y => y.ZLSCH, opt => { opt.MapFrom(x => x.forma_pagamento); })
                .ForMember(y => y.XREF1_HD, opt => { opt.MapFrom(x => x.identificacao_leilao_patio_lote); })
                .ForMember(y => y.WRBTR, opt => { opt.MapFrom(x => x.montante_bruto); })
                .ForMember(y => y.XREF2_HD, opt => { opt.MapFrom(x => x.numero_boleto_pagamento); })
                .ForMember(y => y.BKTXT, opt => { opt.MapFrom(x => x.numero_ordem_interna); })
                .ForMember(y => y.WRBTR_DESCSpecified, opt => { opt.MapFrom(x => x.opcao_valor_desconto); })
                .ForMember(y => y.WRBTR_PAG_MAIORSpecified, opt => { opt.MapFrom(x => x.opcao_valor_pagamento_maior); })
                .ForMember(y => y.WRBTR_TARIFSpecified, opt => { opt.MapFrom(x => x.opcao_valor_tarifa_bancaria); })
                .ForMember(y => y.IDTRANSACAO, opt => { opt.MapFrom(x => x.transacao); })
                .ForMember(y => y.WRBTR_COM, opt => { opt.MapFrom(x => x.valor_comissao); })
                .ForMember(y => y.WRBTR_DESC, opt => { opt.MapFrom(x => x.valor_desconto); })
                .ForMember(y => y.WRBTR_LOTE, opt => { opt.MapFrom(x => x.valor_lote); })
                .ForMember(y => y.WRBTR_PAG_MAIOR, opt => { opt.MapFrom(x => x.valor_pagamento_maior); })
                .ForMember(y => y.WRBTR_TARIF, opt => { opt.MapFrom(x => x.valor_tarifa_bancaria); })
                .ForMember(y => y.WRBTR_TAXA, opt => { opt.MapFrom(x => x.valor_taxa_administrativa); })
                .ForMember(y => y.WRBTR_PAG, opt => { opt.MapFrom(x => x.valor_total_pago); })
                ;

            });
        }
    }
}
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente