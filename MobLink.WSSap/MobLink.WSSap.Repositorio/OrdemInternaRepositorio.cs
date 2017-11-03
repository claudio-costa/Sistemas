using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WSSap.Repositorio
{
    public class OrdemInternaRepositorio : SapRepositorio
    {
        protected si_ordem_interna_requestService.si_ordem_interna_requestService SERVICO_SAP_ORDEM_INTERNA;
        protected si_ordem_interna_requestService.dt_titulo_ord_int_fb70 ENTIDADE_SAP_FB70;

        public OrdemInternaRepositorio()
        {
            CredentialCache Credenciais;

            SERVICO_SAP_ORDEM_INTERNA = new si_ordem_interna_requestService.si_ordem_interna_requestService();

            Credenciais = new CredentialCache
            {
                {
                    new Uri(SERVICO_SAP_ORDEM_INTERNA.Url),
                    "Basic",
                    new NetworkCredential()
                    {
                        UserName = Framework.Setup.WebServices.WsSap(Framework.Util.DetectarAmbiente()).Usuario,
                        Password = Framework.Setup.WebServices.WsSap(Framework.Util.DetectarAmbiente()).Senha
                    }
                }
            };

            SERVICO_SAP_ORDEM_INTERNA.Credentials = Credenciais;
            SERVICO_SAP_ORDEM_INTERNA.PreAuthenticate = true;

            ENTIDADE_SAP_FB70 = new si_ordem_interna_requestService.dt_titulo_ord_int_fb70();
        }
        
        public Dominio.Retorno Inserir(si_ordem_interna_requestService.dt_titulo_ord_int_fb70 entrada)
        {
            try
            {
                entrada.IDTRANSACAO = GerarTransacaoSap().ToString();

                RegistrarSolicitacao(Convert.ToInt32(entrada.IDTRANSACAO), OperacoesSAP.ORDEM_INTERNA, id_lote: Convert.ToInt32(entrada.XREF1_HD));

                SERVICO_SAP_ORDEM_INTERNA.fb70(entrada);
            }
            catch(Exception e)
            {
                return new Dominio.Retorno() { Mensagem = e.Message, Resultado = false };
            }

            return new Dominio.Retorno() { Mensagem = "OK", Resultado = true };
        }
    }
}
