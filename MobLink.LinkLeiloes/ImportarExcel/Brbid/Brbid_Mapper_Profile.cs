using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportarExcel.Brbid
{
    public class Brbid_Mapper_Profile : Profile
    {
        public static void AtivarProfile()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<Brbid_Mapper_Profile>();
            });
        }

        //protected override void Configure()
        //{
        //    Mapper.CreateMap<Brbid.Brbid_Arrematante, MobLink.LinkLeiloes.Dominio.Arrematante>()

        //    .ForMember(y => y.lote, opt => { opt.MapFrom(x => x.LOTE); })
        //    .ForMember(y => y.leilao, opt => { opt.MapFrom(x => x.LEILAO); })
        //    //.ForMember(y => y.placa, opt => { opt.MapFrom(x => (string.IsNullOrEmpty(x.PLACA) && x.PLACA.Length > 6) ? string.Empty : x.PLACA.Substring(0, 7)); })
        //    .ForMember(y => y.placa, opt => { opt.MapFrom(x => x.PLACA); })
        //    .ForMember(y => y.chassi, opt => { opt.MapFrom(x => x.CHASSI); })
        //    .ForMember(y => y.nome_arrematante, opt => { opt.MapFrom(x => string.IsNullOrEmpty(x.NOME_ARREMATANTE) ? string.Empty : x.NOME_ARREMATANTE.ToUpper() ); })
        //    .ForMember(y => y.cpf, opt => { opt.MapFrom(x => x.CPF_CNPJ); })
        //    .ForMember(y => y.cnpj, opt => { opt.MapFrom(x => x.CPF_CNPJ); })
        //    .ForMember(y => y.fone_1, opt => { opt.MapFrom(x => x.TELEFONE_FIXO); })
        //    .ForMember(y => y.fone_2, opt => { opt.MapFrom(x => x.TELEFONE_CELULAR); })
        //    .ForMember(y => y.email, opt => { opt.MapFrom(x => x.EMAIL); })
        //    .ForMember(y => y.logradouro, opt => { opt.MapFrom(x => x.LOGRADOURO); })
        //    .ForMember(y => y.numero, opt => { opt.MapFrom(x => x.NUMERO); })
        //    .ForMember(y => y.complemento, opt => { opt.MapFrom(x => x.COMPLEMENTO); })
        //    .ForMember(y => y.bairro, opt => { opt.MapFrom(x => x.BAIRRO); })
        //    .ForMember(y => y.cidade, opt => { opt.MapFrom(x => x.CIDADE); })
        //    .ForMember(y => y.estado, opt => { opt.MapFrom(x => x.ESTADO); })
        //    .ForMember(y => y.cep, opt => { opt.MapFrom(x => x.CEP); })
        //    .ForMember(y => y.status_boleto, opt => { opt.MapFrom(x => x.STATUS); })
        //    .ForMember(y => y.avaliacao, opt => { opt.MapFrom(x => x.AVALIACAO); })
        //    .ForMember(y => y.arrematacao, opt => { opt.MapFrom(x => x.ARREMATACAO); })
        //    .ForMember(y => y.outras_taxas, opt => { opt.MapFrom(x => x.TAXAS_FIXAS); })
        //    .ForMember(y => y.valor_total, opt => { opt.MapFrom(x => x.TOTAL_COM_TAXAS); })
        //    .ForMember(y => y.valor_pago, opt => { opt.MapFrom(x => x.TOTAL_COM_TAXAS); })
        //    .ForMember(y => y.iss, opt => { opt.MapFrom(x => x.ISS); })
        //    .ForMember(y => y.comissao, opt => { opt.MapFrom(x => x.COMISSAO); })
        //    .ForMember(y => y.cartela, opt => { opt.MapFrom(x => x.CARTELA); })

        //    ;
        //}
    }
}
