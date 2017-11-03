using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.Framework
{
    public static class Util
    {
        public static Dictionary<string, string> LerConfiguracoesAplicacao()
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;

            Dictionary<string, string> lista = new Dictionary<string, string>();

            foreach (var item in appSettings)
            {
                var chave = item.ToString();
                var valor = appSettings.Get(chave);

                lista.Add(chave, valor);
            }

            return lista;
        }

        public static string DetectarConexao()
        {
            try
            {
                return LerConfiguracoesAplicacao()["CONEXAO"];
            }
            catch (Exception e)
            {
                throw new Exception("NÃO FOI POSSÍVEL LER A CHAVE \"CONEXAO\" NO ARQUIVO DE CONFIGURAÇÃO." + e.Message);
            }
        }

        public static string LerConfiguracao(string chave)
        {
            try
            {
                return LerConfiguracoesAplicacao()[chave];
            }
            catch (Exception e)
            {
                throw new Exception("NÃO FOI POSSÍVEL LER A CHAVE NO ARQUIVO DE CONFIGURAÇÃO." + e.Message);
            }
        }

        public static string LerConnectionString(string chave)
        {
            ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;

            try
            {
                return connectionStrings[chave].ConnectionString;
            }
            catch (Exception e)
            {
                throw new Exception("NÃO FOI POSSÍVEL LER A CHAVE NA SEÇÃO." + e.Message);
            }
        }

        public static Ambientes DetectarAmbiente()
        {
            var configuracoes = LerConfiguracoesAplicacao();

            try
            {
                var tipoAmbiente = configuracoes["AMBIENTE"];

                Ambientes Ambiente = (Ambientes)Enum.ToObject(typeof(Ambientes), Convert.ToChar(tipoAmbiente));

                return Ambiente;
            }
            catch (Exception e)
            {
                throw new Exception("NÃO FOI POSSÍVEL LER A CHAVE \"AMBIENTE\" NO ARQUIVO DE CONFIGURAÇÃO." + e.Message);
            }
        }

        public static Sistemas DetectarSistema()
        {
            var configuracoes = LerConfiguracoesAplicacao();

            try
            {
                var tipoSistema = configuracoes["SISTEMA"];

                Sistemas Sistema = (Sistemas)Enum.ToObject(typeof(Sistemas), Convert.ToChar(tipoSistema));

                return Sistema;
            }
            catch (Exception e)
            {
                throw new Exception("NÃO FOI POSSÍVEL LER A CHAVE \"SISTEMA\" NO ARQUIVO DE CONFIGURAÇÃO." + e.Message);
            }
        }

        public static T DetectarBase<T>()
        {
            var configuracoes = LerConfiguracoesAplicacao();

            try
            {
                var tipoAmbiente = configuracoes["DATABASE"];

                //T Bd = (T)Enum.ToObject(typeof(T), Convert.ToChar(tipoAmbiente));

                T Bd = (T)Enum.ToObject(typeof(T), Convert.ToInt32(tipoAmbiente));

                return Bd;
            }
            catch (Exception e)
            {
                throw new Exception("NÃO FOI POSSÍVEL LER A CHAVE \"BD\" NO ARQUIVO DE CONFIGURAÇÃO." + e.Message);
            }
        }
    }
}
