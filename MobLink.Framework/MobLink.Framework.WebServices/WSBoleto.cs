using MobLink.Framework.Setup;
using System;

namespace MobLink.Framework.WebServices
{
    public class WSBoleto
    {
        private _WSBoleto.WsBoleto ws;
        private Ambientes _Ambiente;
        private CredencialWs _Par;

        public WSBoleto(Ambientes Ambiente)
        {
            this._Ambiente = Ambiente;

            try
            {
                ws = new _WSBoleto.WsBoleto();
                ws.Timeout = 120000; //2 MIN

                _Par = Setup.WebServices.WsBoleto(Ambiente);

                ws.Url = _Par.Url;
            }
            catch
            {
                throw new Exception("ERRO AO INICIALIZAR O WEBSERVICE WSBOLETOS");
            }
        }

        ~WSBoleto()
        {
            ws.Dispose();
        }
        
        public byte[] BoletoBancosRetornoLinha(_WSBoleto.BoletoTodos Boleto, string Tipo, out string Linha, out int Linha_Id, bool IsDev = true)
        {
            return ws.BoletoBancosRetornoLinha(Boleto, _Par.Usuario, _Par.Senha, Tipo, IsDev, out Linha, out Linha_Id);
        }

    }
}
