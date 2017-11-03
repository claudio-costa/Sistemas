using AutoMapper;
using MobLink.Framework;
using MobLink.LinkLeiloes.Repositorio;
using System.Linq;

namespace ImportarExcel.AutoMapperProfiles
{
    public class TransguardProfile : Profile
    {
        public static void AtivarProfiles()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<TransguardProfile>();
            });
        }

        private string ConsultaMultaRenainf(string renavam, string placa)
        {
            var rep = new Repositorio();

            var sql = string.Format("SELECT 1 FROM DETALHE_RENAINF WHERE CD_REN_VEI_INF = '{0}' OR PL_VEI_INF_ = '{1}'", renavam, placa);

            return RepositorioGlobal.Util.ConsultaGenerica(Util.DetectarConexao(), sql).ConverterParaLista<int>().Count > 0 ? "S" : "N";
        }

        private string ConsultaRouboFurto(string placa, string chassi)
        {
            var rep = new Repositorio();

            var sql = string.Format("SELECT 1 FROM SITUACAO_ROUBOFURTO_BIN WHERE PLACA = '{0}' OR CHASSI = '{1}'", placa, chassi);

            return RepositorioGlobal.Util.ConsultaGenerica(Util.DetectarConexao(), sql).ConverterParaLista<int>().Count > 0 ? "S" : "N";
        }

        //protected override void Configure()
        //{
        //    Mapper.CreateMap<SITUACAO_ROUBOFURTO_BIN, MobLink.LinkLeiloes.Dominio.Restricao>();

        //    Mapper.CreateMap<RESTRICOES, MobLink.LinkLeiloes.Dominio.Restricao>()

        //    .ForMember(y => y.codigo, opt => { opt.MapFrom(x => x.RESTRICAO); })
        //    .ForMember(y => y.restricao, opt => { opt.MapFrom(x => x.DESC_RESTRICAO); })
        //    .ForMember(y => y.restricao, opt => { opt.MapFrom(x => x.DESC_RESTRICAO); })

        //    ;

        //    Mapper.CreateMap<CADASTRADOS, MobLink.LinkLeiloes.Dominio.Transacao005>()

        //        .ForMember(p => p.Placa, opt => { opt.MapFrom(x => x.PLACA); })
        //        .ForMember(p => p.PlacaNova, opt => { opt.MapFrom(x => x.PLACA); })
        //        .ForMember(p => p.Chassi, opt => { opt.MapFrom(x => x.CHASSI); })
        //        .ForMember(p => p.ChassiRemarcado, opt => { opt.MapFrom(x => x.CHASSI); })
        //        .ForMember(x => x.Renavam, opt => { opt.MapFrom(x => x.RENAVAM); })
        //        .ForMember(x => x.AnoModelo, opt => { opt.MapFrom(x => x.ANO_MODELO); })
        //        .ForMember(x => x.AnoFabricacao, opt => { opt.MapFrom(x => x.ANO_FABRIC); })
        //        .ForMember(x => x.DescricaoTipo, opt => { opt.MapFrom(x => x.DESCRICAO_TIPO); })
        //        .ForMember(x => x.DescricaoMarca, opt => { opt.MapFrom(x => x.DESC_MARCA); })
        //        .ForMember(x => x.DescricaoCor, opt => { opt.MapFrom(x => x.DESCRICAO_COR); })
        //        .ForMember(x => x.DescricaoCombustivel, opt => { opt.MapFrom(x => x.DESCRICAO_COMBUSTIVEL); })
        //        .ForMember(x => x.TipoDocumento, opt => { opt.MapFrom(x => x.TIPO_CIC); })
        //        .ForMember(x => x.NumeroCpfCnpj, opt => { opt.MapFrom(x => x.CIC); })
        //        .ForMember(x => x.NomeProprietario, opt => { opt.MapFrom(x => x.PROPRIETARIO); })
        //        .ForMember(x => x.EnderecoProprietario, opt => { opt.MapFrom(x => x.LOGRAD_END_PROPR); })
        //        .ForMember(x => x.BairroProprietario, opt => { opt.MapFrom(x => x.BAIRRO); })
        //        .ForMember(x => x.ComplementoEnderecoProprietario, opt => { opt.MapFrom(x => x.COMPLEM_END_PROPR); })
        //        .ForMember(x => x.NumeroEnderecoProprietario, opt => { opt.MapFrom(x => x.NUMERO_END_PROPR); })
        //        .ForMember(x => x.CepEnderecoProprietario, opt => { opt.MapFrom(x => x.CEP_END_PROPR); })
        //        .ForMember(x => x.DescricaoMunicipioEndereco, opt => { opt.MapFrom(x => x.DESCR_MUN_END_PROPR); })
        //        .ForMember(x => x.UfProprietario, opt => { opt.MapFrom(x => x.UF); })
        //        .ForMember(x => x.CpfCnpjFinanciamentoEfet, opt => { opt.MapFrom(x => x.CIC_GRAVAME); })
        //        .ForMember(x => x.NomeFinanciamentoEfet, opt => { opt.MapFrom(x => x.GRAVAME); })
        //        .ForMember(x => x.EnderecoFinanciamentoEfet, opt => { opt.MapFrom(x => x.LOGRAD_GRAVAME); })
        //        .ForMember(x => x.BairroFinanciamentoEfet, opt => { opt.MapFrom(x => x.COMPLEM_GRAVAME); })
        //        .ForMember(x => x.ComplementoFinanciamentoEfet, opt => { opt.MapFrom(x => x.BAIRRO_GRAVAME); })
        //        .ForMember(x => x.NumeroFinanciamentoEfet, opt => { opt.MapFrom(x => x.CEP_GRAVAME); })
        //        .ForMember(x => x.CepFinanciamentoEfet, opt => { opt.MapFrom(x => x.NUMERO_GRAVAME); })
        //        .ForMember(x => x.MunicipioFinanciamentoEfet, opt => { opt.MapFrom(x => x.DESCR_MUN_END_GRAVAME); })
        //        .ForMember(x => x.UfFinanciamentoEfet, opt => { opt.MapFrom(x => x.UF_GRAVAME); })
        //        .ForMember(x => x.TipoDocumentoComunicadoVenda, opt => { opt.MapFrom(x => x.TIPO_CIC_CV); })
        //        .ForMember(x => x.CpfCnpjComunicadoVenda, opt => { opt.MapFrom(x => x.CIC_CV); })
        //        .ForMember(x => x.DataVendaComunicadoVenda, opt => { opt.MapFrom(x => x.DT_VENDA_CV); })
        //        .ForMember(x => x.NomeComunicacaoVenda, opt => { opt.MapFrom(x => x.NOME_CV); })
        //        .ForMember(x => x.EnderecoComunicadoVenda, opt => { opt.MapFrom(x => x.LOGRAD_END_CV); })
        //        .ForMember(x => x.BairroComunicadoVenda, opt => { opt.MapFrom(x => x.BAIRRO_END_CV); })
        //        .ForMember(x => x.ComplementoComunicadoVenda, opt => { opt.MapFrom(x => x.COMPLEM_END_CV); })
        //        .ForMember(x => x.NumeroComunicadoVenda, opt => { opt.MapFrom(x => x.NUMERO_END_CV); })
        //        .ForMember(x => x.CepComunicadoVenda, opt => { opt.MapFrom(x => x.CEP_END_CV); })
        //        .ForMember(x => x.MunicipioComunicadoVenda, opt => { opt.MapFrom(x => x.DESCR_MUN_END_CV); })
        //        .ForMember(x => x.UfComunicadoVenda, opt => { opt.MapFrom(x => x.UF_END_CV); })
        //        .ForMember(x => x.NumeroMotor, opt => { opt.MapFrom(x => x.NUMERO_DO_MOTOR); })
        //        .ForMember(x => x.PlacaAnterior, opt => { opt.MapFrom(x => x.PLACA_ANTERIOR); })
        //        //.ForMember(x => x.Flag_notificar_proprietario, opt => { opt.MapFrom(x => "S"); })
        //        //.ForMember(x => x.flag_notificar_financeira, opt => { opt.MapFrom(x => "S"); })
        //        //.ForMember(x => x.flag_notificar_comunicado, opt => { opt.MapFrom(x => "S"); })
        //        .ForMember(x => x.Observacoes, opt => { opt.MapFrom(x => x.MENSAGEM); })
        //        .ForMember(x => x.Retorno, opt => { opt.MapFrom(x => "S"); })
        //        .ForMember(x => x.NomeAgenteFinanceiro, opt => { opt.MapFrom(x => x.NOME_AGENTE); })
        //        .ForMember(x => x.CpfCnpjAgeteFinanceiro, opt => { opt.MapFrom(x => x.CGC_AGENTE); })
        //        .ForMember(x => x.NomeFinanciadoSng, opt => { opt.MapFrom(x => x.NOME_FINANCIADO); })
        //        .ForMember(x => x.CpfCnpjFinanciadoSng, opt => { opt.MapFrom(x => x.CPF_CGC_FINANCIADO); })
        //        .ForMember(x => x.DescricaoCategoria, opt => { opt.MapFrom(x => x.CATEGORIA); })
        //        .ForMember(x => x.IndicacaoMultasRenainf, opt => { opt.MapFrom(x => ConsultaMultaRenainf(x.RENAVAM, x.PLACA)); })
        //        .ForMember(y => y.IndicacaoRouboFurto, opt => { opt.MapFrom(x => ConsultaRouboFurto(x.RENAVAM, x.CHASSI)); })


                
        //        ;

        //}
    }
}
