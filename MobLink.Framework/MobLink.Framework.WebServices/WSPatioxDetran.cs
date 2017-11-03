using System;

namespace MobLink.Framework.WebServices
{
    public class WSPatioxDetran
    {
        private _WSPatioxDetran.WSPatioxDetran ws;

        public WSPatioxDetran(Ambientes Ambiente)
        {
            try
            {
                ws = new _WSPatioxDetran.WSPatioxDetran();
                ws.Timeout = 120000; //2 MIN

                var p = Setup.WebServices.WsPatioXDetran(Ambiente); // Parametros.URL.Webservices.WSPatioxDetran(Ambiente);

                ws.Url = p.Url;

                ws.Credentials = new System.Net.NetworkCredential(p.Usuario, p.Senha);
                ws.Timeout = 1000000000; // 10 segundos
            }
            catch
            {
                throw new Exception("Erro ao inicializar o webservice WSPatioxDetran");
            }
        }

        ~WSPatioxDetran()
        {
            ws.Dispose();
        }

        public string ConsultaVeiculo(string Placa, string Operador, string Estado = "")
        {
            return ws.ConsultarVeiculo(Placa, Operador, Estado);
        }

        public string ConsultaVeiculoParaLeilao(string Operador, string Placa = null, string Chassi = null)
        {
            return ws.ConsultarVeiculoParaLeilao(new _WSPatioxDetran.VeiculoLeilao() { chassi = Chassi, login = Operador, operador = Operador, placa = Placa });
        }

        public string InclusaoVeiculoPatio(string Operador, string Placa = null, string Chassi = null)
        {
            return ws.InclusaoVeiculoPatio(new _WSPatioxDetran.Veiculoinclusao()
            {
                
            });
        }
    }
}
