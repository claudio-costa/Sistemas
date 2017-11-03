using MobLink.Framework.Setup;
using System;
using System.Collections.Generic;

namespace MobLink.Framework.WebServices
{
    public class FinanceiroDetalhamento_Output
    {
        public string leilao { get; set; }
        public string lote { get; set; }
        public string dataemissao { get; set; }
        public string nota_v { get; set; }
        public string placa_v { get; set; }
        public string chassis { get; set; }
        public string avaliacao_v { get; set; }
        public string arrematacao { get; set; }
        public string iss_v { get; set; }
        public string comissao_v { get; set; }
        public string taxasfixas { get; set; }
        public string total { get; set; }
        public string nomearrematante { get; set; }
        public string cpfcnpj { get; set; }
        public string telefone1 { get; set; }
        public string telefone2 { get; set; }
        public string email { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
        public string status { get; set; }
        public string numero_processo { get; set; }
        public string numero_boleto { get; set; }
    }

    public class WSVipBoleto
    {
        private _VipBoletoWS.RodandoLegal ws;
        private CredencialWs par;
        
        public WSVipBoleto(Ambientes Ambiente)
        {
            try
            {
                ws = new _VipBoletoWS.RodandoLegal();
                //ws.Timeout = 120000; //2 MIN

                par = Setup.WebServices.WsVipBoleto(Ambiente); // Parametros.URL.Webservices.WSVipBoleto(Ambiente);

                ws.Url = par.Url;

                ws.Credentials = new System.Net.NetworkCredential(par.Usuario, par.Senha);
            }
            catch
            {
                throw new Exception("Erro ao inicializar o webservice VipBoletoWS");
            }
        }

        ~WSVipBoleto()
        {
            ws.Dispose();
        }

        public List<FinanceiroDetalhamento_Output> FinanceiroDetalhamento(string DescricaoLeilao)
        {
            var ret = ws.FinanceiroFechamento(par.Usuario, par.Senha, DescricaoLeilao);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<FinanceiroDetalhamento_Output>>(ws.FinanceiroFechamento(par.Usuario, par.Senha, DescricaoLeilao));
        }

    }
}
