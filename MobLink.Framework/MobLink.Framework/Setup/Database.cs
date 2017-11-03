using System;

namespace MobLink.Framework.Setup
{
    public static class Database
    {
        public static string dbMobLinkDepositoPublico(Ambientes Ambiente)
        {
            switch (Ambiente)
            {
                case Ambientes.Desenvolvimento:
                    return @"Server = 179.107.47.90; Database=dbMobLinkDepositoPublicoDesenvolvimento; User Id=sapdev; Password = Buracica#;";

                case Ambientes.Producao:
                    return @"Server = 179.107.47.90; Database=dbMobLinkDepositoPublicoProducao; User Id=sapdev; Password = Buracica#;";

                default: throw new Exception("AMBIENTE INFORMADO DESCONHECIDO");
            }
        }

        public static string dbMobLinkLeilao(Ambientes Ambiente)
        {
            switch (Ambiente)
            {
                case Ambientes.Desenvolvimento:
                    return @"Server = 179.107.47.90; Database=dbLeilao; User Id=user_leilao; Password = Buracica12#;";

                case Ambientes.Producao:
                    return @"Server = 179.107.47.90; Database=dbLeilao; User Id=user_leilao; Password = Buracica12#;";

                default: throw new Exception("AMBIENTE INFORMADO DESCONHECIDO");
            }
        }

        public static string dbMobLinkSap(Ambientes Ambiente)
        {
            switch (Ambiente)
            {
                case Ambientes.Desenvolvimento:
                    return @"Server = 179.107.47.90; Database=dbMobLinkSAPDev; User Id=sapdev; Password = Buracica#;";

                case Ambientes.Producao:
                    return @"Server = 179.107.47.90; Database=dbMobLinkSAP; User Id=sapprod; Password = Buracica@#;";

                default: throw new Exception("AMBIENTE INFORMADO DESCONHECIDO");
            }
        }
    }
}
