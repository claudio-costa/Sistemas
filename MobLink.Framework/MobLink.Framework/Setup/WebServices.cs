using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.Framework.Setup
{
    public static class WebServices
    {
        public static CredencialWs WsPatioXDetran(Ambientes Ambiente)
        {
            switch (Ambiente)
            {
                case Ambientes.Desenvolvimento:
                    return new CredencialWs()
                    {
                        Url = "http://179.107.47.90:94/WSPatioxDetran.asmx",
                        Usuario = @"MLRJ01\user_ws_sap",
                        Senha = "Buracica12#"
                    };

                case Ambientes.Producao:
                    return new CredencialWs()
                    {
                        Url = "http://179.107.47.90:94/WSPatioxDetran.asmx",
                        Usuario = @"MLRJ01\user_ws_sap",
                        Senha = "Buracica12#"
                    };

                default: throw new Exception("AMBIENTE INFORMADO DESCONHECIDO");
            }
        }

        public static CredencialWs WsBoleto(Ambientes Ambiente)
        {
            switch (Ambiente)
            {
                case Ambientes.Desenvolvimento:
                    return new CredencialWs()
                    {
                        Url = "http://179.107.47.90:8088/wsboleto.asmx",
                        Usuario = "MOBLINK",
                        Senha = "studio55@"
                    };

                case Ambientes.Producao:
                    return new CredencialWs()
                    {
                        Url = "http://179.107.47.90:8083/wsboleto.asmx",
                        Usuario = "MOBLINK",
                        Senha = "studio55@"
                    };

                default: throw new Exception("AMBIENTE INFORMADO DESCONHECIDO");
            }
        }

        public static CredencialWs WsSap(Ambientes Ambiente)
        {
            switch (Ambiente)
            {
                case Ambientes.Desenvolvimento:
                    return new CredencialWs()
                    {
                        Url = "http://179.107.47.90:82/",
                        Usuario = "DSIN_PID",
                        Senha = "dsin2pid"
                    };

                case Ambientes.Producao:
                    return new CredencialWs()
                    {
                        Url = "http://179.107.47.90:83/",
                        Usuario = "linkpatios_pip",
                        Senha = "linkpatios2pip"
                    };

                default: throw new Exception("AMBIENTE INFORMADO DESCONHECIDO");
            }
        }

        public static CredencialWs WsVipBoleto(Ambientes Ambiente)
        {
            switch (Ambiente)
            {
                case Ambientes.Desenvolvimento:
                    return new CredencialWs()
                    {
                        //Url = "http://ws-boletos.vipleiloes.com/VipBoletoWS.asmx",
                        Url = "http://ws-parceiros.vipleiloes.com/rodandolegal.asmx",

                        //Usuario = "viptestews",
                        //Senha = "vipteste@servico"

                        Usuario = "vip@rodando",
                        Senha= "viprodando@servico"
                    };

                case Ambientes.Producao:
                    return new CredencialWs()
                    {
                        Url = "http://ws-boletos.vipleiloes.com/VipBoletoWS.asmx",
                        Usuario = "viptestews",
                        Senha = "vipteste@servico"
                    };
                default: throw new Exception("AMBIENTE INFORMADO DESCONHECIDO");
            }
        }

        public static CredencialWs WsDsin(Ambientes Ambiente)
        {
            switch (Ambiente)
            {
                case Ambientes.Desenvolvimento:
                    return new CredencialWs()
                    {
                        Url = "http://patiodemo.dsin.com.br/webService/webServiceSiol.php",
                        Usuario = "LeilaoRL",
                        Senha = "SLeilaoRL"
                    };

                case Ambientes.Producao:
                    return new CredencialWs()
                    {
                        Url = "http://patio.dsin.com.br/webService/webServiceSiol.php",
                        Usuario = "LeilaoRL",
                        Senha = "SLeilaoRL"
                    };

                default: throw new Exception("AMBIENTE INFORMADO DESCONHECIDO");
            }
        }
    }
}
