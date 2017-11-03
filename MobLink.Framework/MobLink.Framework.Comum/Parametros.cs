using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using static MobLink.Framework.Comum.Global;

namespace MobLink.Framework.Comum
{
    public static class Parametros
    {
        public static class Email
        {
            public static class SMTP
            {
                public const string Host = "smtp.gmail.com";
                public const int Porta = 587;
                public const bool RequerAutenticacao = true;
                public const bool RequerSSL = true;
            }

            public static class IMAP
            {
                public const string Host = "imap.gmail.com";
                public const int Porta = 993;
            }

            public static class EmailsDoSistema
            {
                public static class WebAtendimento
                {
                    public const string Email = "system@mob-link.net.br";
                    public const string Senha = "Studio55#";
                    public const string NomeAmigavel = "BOLETO LINKPÁTIOS";
                }
            }
        }

        public static class URL
        {
            public static class Webservices
            {
                public static Global.ParametrosServico WSVipBoleto(Ambientes.TiposAmbiente Ambiente)
                {
                    switch (Ambiente)
                    {
                        case Global.Ambientes.TiposAmbiente.Desenvolvimento:
                            return new ParametrosServico();

                        case Global.Ambientes.TiposAmbiente.Producao:
                            return new ParametrosServico()
                            {
                                URL = "http://ws-boletos.vipleiloes.com/VipBoletoWS.asmx",
                                Usuario = "viptestews",
                                Senha = "vipteste@servico"
                            };

                        default:
                            throw new Exception("Ambiente informado desconhecido");
                    }
                }

                public static Global.ParametrosServico WSDsin(Ambientes.TiposAmbiente Ambiente)
                {
                    switch (Ambiente)
                    {
                        case Global.Ambientes.TiposAmbiente.Desenvolvimento:
                            return new ParametrosServico()
                            {
                                URL = "http://patiodemo.dsin.com.br/webService/webServiceSiol.php",
                                Usuario = "LeilaoRL",
                                Senha = "SLeilaoRL"
                            };

                        case Global.Ambientes.TiposAmbiente.Producao:
                            return new ParametrosServico()
                            {
                                URL = "http://patio.dsin.com.br/webService/webServiceSiol.php",
                                Usuario = "LeilaoRL",
                                Senha = "SLeilaoRL"
                            };

                        default:
                            throw new Exception("Ambiente informado desconhecido");
                    }
                }

                public static Global.ParametrosServico WSPatioxDetran(Ambientes.TiposAmbiente Ambiente)
                {
                    switch (Ambiente)
                    {
                        case Global.Ambientes.TiposAmbiente.Desenvolvimento:
                            return new ParametrosServico()
                            {
                                URL = "http://179.107.47.90:94/WSPatioxDetran.asmx",
                                Usuario = @"MLRJ01\user_ws_sap",
                                Senha = "Buracica12#"
                            };

                        case Global.Ambientes.TiposAmbiente.QaD:
                            return new ParametrosServico()
                            {
                                URL = "http://179.107.47.90:94/WSPatioxDetran.asmx",
                                Usuario = @"MLRJ01\user_ws_sap",
                                Senha = "Buracica12#"
                            };

                        case Global.Ambientes.TiposAmbiente.Producao:
                            return new ParametrosServico()
                            {
                                URL = "http://179.107.47.90:94/WSPatioxDetran.asmx",
                                Usuario = @"MLRJ01\user_ws_sap",
                                Senha = "Buracica12#"
                            };

                        default:
                            throw new Exception("Ambiente informado desconhecido");
                    }
                }

                public static Global.ParametrosServico WSBoleto(Ambientes.TiposAmbiente Ambiente)
                {
                    switch (Ambiente)
                    {
                        case Global.Ambientes.TiposAmbiente.Desenvolvimento:
                            return new ParametrosServico();

                        case Global.Ambientes.TiposAmbiente.Producao:
                            return new ParametrosServico()
                            {
                                URL = "http://179.107.47.90:8088/wsboleto.asmx",
                                Usuario = "MOBLINK",
                                Senha = "studio55@"
                            };

                        default:
                            throw new Exception("Ambiente informado desconhecido");
                    }
                }

                public static Global.ParametrosServico WSSap(Ambientes.TiposAmbiente Ambiente)
                {
                    switch (Ambiente)
                    {
                        case Global.Ambientes.TiposAmbiente.Desenvolvimento:
                            return new ParametrosServico()
                            {
                                URL = "http://179.107.47.90:82/",
                                Usuario = "DSIN_PID",
                                Senha = "dsin2pid"
                            };

                        case Global.Ambientes.TiposAmbiente.QaD:
                            return new ParametrosServico()
                            {
                                URL = "http://179.107.47.90:82/",
                                Usuario = "DSIN_PID",
                                Senha = "dsin2pid"
                            };

                        case Global.Ambientes.TiposAmbiente.Producao:
                            return new ParametrosServico()
                            {
                                URL = "http://179.107.47.90:83/",
                                Usuario = "linkpatios_pip",
                                Senha = "linkpatios2pip"
                            };

                        default:
                            throw new Exception("Ambiente informado desconhecido");
                    }
                }
            }

            public static class Wsdl
            {
                public static Global.ParametrosServico Sap(Ambientes.TiposAmbiente Ambiente)
                {
                    switch (Ambiente)
                    {
                        case Global.Ambientes.TiposAmbiente.Desenvolvimento:
                            return new ParametrosServico()
                            {
                                URL = "http://179.107.47.90:82/wsdl/",
                                Usuario = "DSIN_PID",
                                Senha = "dsin2pid"
                            };

                        case Global.Ambientes.TiposAmbiente.QaD:
                            return new ParametrosServico()
                            {
                                //URL = "http://179.107.47.90:82/wsdl/",
                                URL = "http://srvsappidev:50100/dir/wsdl?p=ic/520e032e894130e6ba684e11c3cb3496",
                                //Usuario = "DSIN_PID",
                                //Senha = "dsin2pid"
                                Usuario = "LINKPATIOS_PID",
                                Senha = "linkpatios2pid"
                            };

                        case Global.Ambientes.TiposAmbiente.Producao:
                            return new ParametrosServico()
                            {
                                URL = "http://179.107.47.90:83/wsdl/",
                                Usuario = "linkpatios_pip",
                                Senha = "linkpatios2pip"
                            };

                        default:
                            throw new Exception("Ambiente informado desconhecido");
                    }
                }
            }
        }

        public static class ConnectionStrings
        {
            public static string dbMobLinkDepositoPublico(Ambientes.TiposAmbiente Ambiente)
            {
                switch (Ambiente)
                {
                    case Global.Ambientes.TiposAmbiente.Desenvolvimento:
                        return @"Server = 179.107.47.90; Database=dbMobLinkDepositoPublicoDesenvolvimento; User Id=sapdev; Password = Buracica#;";
                    case Global.Ambientes.TiposAmbiente.QaD:
                        return @"Server = 179.107.47.90; Database=dbMobLinkDepositoPublicoDesenvolvimento; User Id=sapdev; Password = Buracica#;";
                    case Global.Ambientes.TiposAmbiente.Producao:
                        return @"Server = 179.107.47.90; Database=dbMobLinkDepositoPublicoProducao; User Id=sapdev; Password = Buracica#;";
                    default:
                        throw new Exception("Ambiente informado desconhecido");
                }
            }

            public static string dbMobLinkLeilao(Ambientes.TiposAmbiente Ambiente)
            {
                switch (Ambiente)
                {
                    case Global.Ambientes.TiposAmbiente.Desenvolvimento:
                        return @"Server = 179.107.47.90; Database=dbLeilao; User Id=user_leilao; Password = Buracica12#;";
                    case Global.Ambientes.TiposAmbiente.QaD:
                        return @"Server = 179.107.47.90; Database=dbLeilao; User Id=user_leilao; Password = Buracica12#;";
                    case Global.Ambientes.TiposAmbiente.Producao:
                        return @"Server = 179.107.47.90; Database=dbLeilao; User Id=user_leilao; Password = Buracica12#;";
                    default:
                        throw new Exception("Ambiente informado desconhecido");
                }
            }

            public static string dbMobLinkSap(Ambientes.TiposAmbiente Ambiente)
            {
                switch (Ambiente)
                {
                    case Global.Ambientes.TiposAmbiente.Desenvolvimento:
                        return @"Server = 179.107.47.90; Database=dbMobLinkSAPDev; User Id=sapdev; Password = Buracica#;";
                    case Global.Ambientes.TiposAmbiente.QaD:
                        return @"Server = 179.107.47.90; Database=dbMobLinkSAPDev; User Id=sapdev; Password = Buracica#;";
                    case Global.Ambientes.TiposAmbiente.Producao:
                        return @"Server = 179.107.47.90; Database=dbMobLinkSAP; User Id=sapprod; Password = Buracica@#;";
                    default:
                        throw new Exception("Ambiente informado desconhecido");
                }
            }
        }
    }
}
