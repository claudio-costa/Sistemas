

namespace ConsoleApp1
{


    class Program
    {
        

        public class Teste : MobLink.Framework.Database.DbSqlServer
        {
            public Teste() : base(MobLink.Framework.Util.LerConfiguracao("CONEXAO_SAP_DEV"))
            {
                var teste = ExecutaSQL_ScopeIdentity(string.Format(@"INSERT INTO dbo.tb_sap_erros (id_transacao_sap, metodo, mensagem)
                            VALUES ({0}, '{1}', '{2}')", 1234, "teste", "teste"));
            }
        }

        static void Main(string[] args)
        {
            var w = new MobLink.Framework.WebServices.WSDsin(MobLink.Framework.Ambientes.Producao).ConsultaDadosLotesLeilao("DT27.17");

            var cep = MobLink.Framework.WebServices.WebserviceCorreios.ConsultaCEP("24800001");

            Teste teste = new Teste();

            //MobLink.Framework.WebServices.WSPatioxDetran wSPatioxDetran = new MobLink.Framework.WebServices.WSPatioxDetran(Ambientes.Producao);

            //var retornoconsulta = wSPatioxDetran.ConsultaVeiculoParaLeilao("ROOT", "HQJ2014", null);


            //MobLink.Framework.WebServices.WSVipBoleto wSVipBoleto = new MobLink.Framework.WebServices.WSVipBoleto(Ambientes.Desenvolvimento);

            //var resultado = wSVipBoleto.FinanceiroDetalhamento("TRGD02.17");
            
        }
    }
}
