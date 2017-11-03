using System;

namespace MobLink.Framework.Comum
{
    public class Global
    {
        public class Ambientes
        {
            public static TiposAmbiente SelecionarAmbiente()
            {
                var configuracoes = CCFW.Funcoes.EnumUtils.ReadAllSettings();

                try
                {
                    TiposAmbiente Ambiente = (TiposAmbiente)Enum.ToObject(typeof(TiposAmbiente), int.Parse(configuracoes["AMBIENTE"]));

                    return Ambiente;
                }
                catch (Exception e)
                {
                    throw new Exception("NÃO FOI POSSÍVEL LER A CHAVE 'AMBIENTE' NO ARQUIVO DE CONFIGURAÇÃO." + e.Message);
                }
            }

            public enum TiposAmbiente
            {
                Desenvolvimento, QaD, Producao
            }
        }

        public enum Sistema
        {
            DepositoPublico, Leilao, Sap
        }

        public struct ParametrosServico
        {
            public string URL { get; set; }
            public string Usuario { get; set; }
            public string Senha { get; set; }
        }
    }
    
}
