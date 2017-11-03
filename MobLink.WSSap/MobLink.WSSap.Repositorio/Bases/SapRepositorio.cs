using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WSSap.Repositorio
{
    public enum OperacoesSAP
    {
        CRIAR_CLIENTE, MODIFICAR_CLIENTE, ORDEM_VENDA, ORDEM_INTERNA
    }

    public class SapRepositorio : BaseRepositorio
    {
        public SapRepositorio() : base(Framework.Util.LerConfiguracao("CONEXAO"))
        {

        }

        protected int GerarTransacaoSap()
        {

            var teste = ConsultaSQL("select db_name()");
            

            string sql = "SELECT MAX(id_transacao_sap) + 1 FROM dbo.tb_sap_solicitacao";

            try
            {
                var result = ExecutaSQL_ScopeIdentity(sql);

                if (result.ToString() == string.Empty)
                    return 1;

                return result;
            }
            catch (Exception e)
            {
                return 1;
            }
        }

        protected void RegistrarSolicitacao(int transacao_sap, OperacoesSAP operacao, int id_grv = 0, int id_atendimento = 0, int id_lote = 0)
        {
            var SQL = new StringBuilder();

            var teste = ConsultaSQL("select db_name()");

            SQL.AppendLine("INSERT INTO dbo.tb_sap_solicitacao(id_transacao_sap, operacao, id_grv, id_atendimento, id_lote)");
            SQL.AppendFormat("VALUES({0}, '{1}', {2}, {3}, {4})", transacao_sap, operacao.ToString(), id_grv, id_atendimento, id_lote);

            try
            {
                var result = ExecutaSQL(SQL.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dominio.Retorno InserirRetornoSap(Dominio.Transacao transacao)
        {
            //if (!ValidarEntrada(ref Transacao)) { return new Dominio.Retorno() { Resultado = false, Mensagem = "" }; }

            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("INSERT INTO tb_sap_retorno (id_transacao_sap, id_documento, mensagens, nota, data_emissao_nota)");
            SQL.AppendFormat("VALUES ({0}, '{1}', '{2}', '{3}', '{4}')", transacao.idTransacao,
                                                                         transacao.retDocId,
                                                                         transacao.retMensagens,
                                                                         transacao.retnota,
                                                                         transacao.retdtemissao);

            try
            {
                var tabela = ExecutaSQL(SQL.ToString());
                return new Dominio.Retorno() { Resultado = true, Mensagem = "" };
            }
            catch (Exception ex)
            {
                return new Dominio.Retorno() { Resultado = false, Mensagem = "Erro ao inserir dados: " + ex };
            }
        }

        protected void RegistrarLog(Exception ex, OperacoesSAP operacao, int IdTransacaoSap = 0)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("INSERT INTO dbo.tb_sap_erros(id_transacao_sap, metodo, mensagem)");
            SQL.AppendFormat("VALUES({0}, '{1}', '{2}')", IdTransacaoSap, operacao.ToString(), ex.Message);

            try
            {
                var result = ExecutaSQL(SQL.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
