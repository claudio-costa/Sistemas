using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobLink.Framework.WebServices;
using System.Linq.Expressions;

namespace MobLink.WebLeilao.Repositorio
{
    public class ArrematanteMapperProfile : Profile
    {
        public static void Iniciar()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<MobLink.Framework.WebServices.FinanceiroDetalhamento_Output, Dominio.Arrematante>()

                .ForMember(y => y.arrematacao, opt => { opt.MapFrom(x => x.arrematacao); })
                .ForMember(y => y.avaliacao, opt => { opt.MapFrom(x => x.avaliacao_v); })
                .ForMember(y => y.bairro, opt => { opt.MapFrom(x => x.bairro); })
                .ForMember(y => y.cep, opt => { opt.MapFrom(x => x.cep); })
                .ForMember(y => y.chassi, opt => { opt.MapFrom(x => x.chassis); })
                .ForMember(y => y.cidade, opt => { opt.MapFrom(x => x.cidade); })
                .ForMember(y => y.comissao, opt => { opt.MapFrom(x => x.comissao_v); })
                .ForMember(y => y.complemento, opt => { opt.MapFrom(x => x.complemento); })
                .ForMember(y => y.cpf, opt => { opt.MapFrom(x => x.cpfcnpj); })
                //.ForMember(y => y.cnpj, opt => { opt.MapFrom(x => x.cpfcnpj); })
                .ForMember(y => y.data_emissao_boleto, opt => { opt.MapFrom(x => x.dataemissao); })
                .ForMember(y => y.email, opt => { opt.MapFrom(x => x.email); })
                .ForMember(y => y.estado, opt => { opt.MapFrom(x => x.estado); })
                .ForMember(y => y.iss, opt => { opt.MapFrom(x => x.iss_v); })
                .ForMember(y => y.leilao, opt => { opt.MapFrom(x => x.leilao); })
                .ForMember(y => y.logradouro, opt => { opt.MapFrom(x => x.logradouro); })
                .ForMember(y => y.lote, opt => { opt.MapFrom(x => x.lote); })
                .ForMember(y => y.nome_arrematante, opt => { opt.MapFrom(x => x.nomearrematante); })
                .ForMember(y => y.numero, opt => { opt.MapFrom(x => x.numero); })
                .ForMember(y => y.numero_boleto, opt => { opt.MapFrom(x => x.numero_boleto); })
                .ForMember(y => y.numero_processo, opt => { opt.MapFrom(x => x.numero_processo); })
                .ForMember(y => y.placa, opt => { opt.MapFrom(x => x.placa_v); })
                .ForMember(y => y.status_boleto, opt => { opt.MapFrom(x => x.status); })
                .ForMember(y => y.taxa_administrativa, opt => { opt.MapFrom(x => x.taxasfixas); })
                .ForMember(y => y.fone_1, opt => { opt.MapFrom(x => x.telefone1); })
                .ForMember(y => y.fone_2, opt => { opt.MapFrom(x => x.telefone2); })
                .ForMember(y => y.valor_total, opt => { opt.MapFrom(x => x.total); })
                ;
            });
        }
    }
}
