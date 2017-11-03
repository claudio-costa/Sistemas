using MobLink.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.LinkLeiloes.Repositorio
{
    public class SapRepositorio : Framework.Database.DbSqlServer
    {
        public SapRepositorio() : base(Framework.Util.LerConfiguracao("CONEXAO_SAP")) { }


        public string RecuperaCodigoClienteSAP(string cpf_cnpj)
        {
            try
            {
                string sql = string.Format(@"
                SELECT mensagens, id_documento
                  FROM tb_sap_retorno 
                 WHERE id_transacao_sap = (SELECT TOP 1 id_transacao_sap 
                                             FROM tb_sap_clientes 
                                            WHERE cpf = '{0}' OR cnpj = '{0}' ORDER BY id_transacao_sap DESC)", cpf_cnpj);

                var res = ConsultaSQL(sql);

                if (res.Rows.Count == 0) return "";

                var id_documento = res.Rows[0]["id_documento"].ToString();

                if (id_documento != "")
                {
                    return id_documento;
                }
                else
                {
                    id_documento = res.Rows[0]["mensagens"].ToString();

                    id_documento = id_documento.Substring(id_documento.IndexOf("Cliente ") + 8);

                    id_documento = id_documento.Substring(0, id_documento.IndexOf(" já"));

                    return id_documento;
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public string RecuperaOrdemFB70SAP(string id_lote)
        {
            string sql = string.Format(@"
            
            SELECT mensagens, id_documento
              FROM tb_sap_retorno 
             WHERE id_transacao_sap = (SELECT TOP 1 id_transacao_sap 
                                         FROM tb_sap_solicitacao
                                        WHERE id_lote = {0}
                                        ORDER BY id_sap_solicitacao DESC)", id_lote);

            var res = ConsultaSQL(sql);

            var id_documento = res.Rows[0]["id_documento"].ToString();

            if (id_documento != "")
            {
                return id_documento;
            }
            else
            {
                id_documento = res.Rows[0]["mensagens"].ToString();

                if (id_documento.Contains("ERRO"))
                    return "";

                id_documento = id_documento.Substring(id_documento.IndexOf("Cliente ") + 8);

                id_documento = id_documento.Substring(0, id_documento.IndexOf(" já"));

                return id_documento;
            }
        }

    }
}
