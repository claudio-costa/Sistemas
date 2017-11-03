using MobLink.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WSSap.Repositorio
{
    public class SapRepositorio___ : Framework.Database.DbSqlServer
    {
        protected Framework.Ambientes Ambiente { get; private set; }

        public SapRepositorio___() : base(Framework.Util.LerConfiguracao("CONEXAO"))
        {
            Ambiente = Framework.Util.DetectarAmbiente();
        }

        private bool ValidarEntrada(ref Dominio.Transacao Transacao)
        {
            if (Transacao.idTransacao <= 0)
                return false;

            if (Transacao.retMensagens.Contains("já existe para a empresa"))
            {
                var palavras = Transacao.retMensagens.Split(' ');
                Transacao.retDocId = palavras[1];
            }

            return true;
        }

        protected string GetTransacaoSap()
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("select max(id_transacao_sap) + 1");
            SQL.AppendLine("from tb_sap_solicitacao");

            try
            {
                var con = Framework.Util.LerConfiguracao("CONEXAO");
                var consulta = ConsultaSQL("SELECT DB_NAME() AS [Current Database];");
                var result = ExecutaSQL(SQL.ToString());

                if (result.ToString() == string.Empty)
                {
                    return "1";
                }

                return result.ToString();
            }
            catch (Exception e)
            {
                return "1";
            }
            finally
            {
                
            }
        }

        protected void InserirSolicitacao(string NumeroTransacao, string Operacao, int id_grv = 0, int id_atendimento = 0)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("INSERT INTO dbo.tb_sap_solicitacao(id_transacao_sap, operacao, id_grv, id_atendimento)");
            SQL.AppendFormat("VALUES({0}, '{1}', {2}, {3})", NumeroTransacao, Operacao, id_grv, id_atendimento);

            try
            {
                AlterarConexao(Util.LerConfiguracao("CONEXAO"));
                var consulta = ConsultaSQL("SELECT DB_NAME() AS [Current Database];");
                var result = ExecutaSQL(SQL.ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        protected Dominio.Retorno InserirRetornoSap(Dominio.Transacao Transacao)
        {
            if (!ValidarEntrada(ref Transacao)) { return new Dominio.Retorno() { Resultado = false, Mensagem = "" }; }

            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("INSERT INTO tb_sap_retorno (id_transacao_sap, id_documento, mensagens, nota, data_emissao_nota)");
            SQL.AppendFormat("VALUES ({0}, '{1}', '{2}', '{3}', '{4}')", Transacao.idTransacao,
                                                                         Transacao.retDocId,
                                                                         Transacao.retMensagens,
                                                                         Transacao.retnota,
                                                                         Transacao.retdtemissao);

            try
            {
                var tabela = ExecutaSQL(SQL.ToString());
                return new Dominio.Retorno() { Resultado = true, Mensagem = "" };
            }
            catch (Exception ex)
            {
                return new Dominio.Retorno() { Resultado = false, Mensagem = "Erro ao inserir dados: " + ex };
                //throw new Exception(Funcoes.LoggerClass.SQLErrorLog(ex, SQL));
            }
            finally
            {
                
            }
        }

        protected void Log(Exception Erro, string Metodo, int IdTransacaoSap = 0)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("INSERT INTO dbo.tb_sap_erros(id_transacao_sap, metodo, mensagem)");
            SQL.AppendFormat("VALUES({0}, '{1}', '{2}')", IdTransacaoSap, Metodo, Erro.Message);

            try
            {
                var result = ExecutaSQL(SQL.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                
            }
        }

        public static Dominio.Retorno IngressaTransacaoSAP(Dominio.Transacao Transacao)
        {
            SapRepositorio___ sap = new SapRepositorio___();
            return sap.InserirRetornoSap(Transacao);
        }
    }
}
