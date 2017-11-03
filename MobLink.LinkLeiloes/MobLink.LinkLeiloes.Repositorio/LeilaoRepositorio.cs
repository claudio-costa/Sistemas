using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MobLink.WebLeilao.Repositorio
{
    public class LeilaoRepositorio : DbSqlServer, ICrud<Leilao, int>
    {
        protected internal LeilaoRepositorio() : base(Util.DetectarConexao())
        {

        }

        public int Alterar(Leilao entidade)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("UPDATE dbo.tb_leilao                                                                ");
            SQL.AppendLine("SET id = @id,                                                                       ");
            SQL.AppendLine("	descricao = @descricao,                                                         ");
            SQL.AppendLine("	nome_local = @nome_local,                                                       ");
            SQL.AppendLine("	numero_diario_oficial = @numero_diario_oficial,                                 ");
            SQL.AppendLine("	ordem_interna_matriz = @ordem_interna_matriz,                                   ");
            SQL.AppendLine("	ordem_interna_leilao = @ordem_interna_leilao,                                   ");
            SQL.AppendLine("	id_leiloeiro = @id_leiloeiro,                                                   ");
            SQL.AppendLine("	id_usuario_cadastro = @id_usuario_cadastro,                                     ");
            SQL.AppendLine("	id_comitente = @id_comitente,                                                   ");
            SQL.AppendLine("	id_expositor = @id_expositor,                                                   ");
            SQL.AppendLine("	id_status = @id_status,                                                         ");
            SQL.AppendLine("	data_leilao = @data_leilao,                                                     ");
            SQL.AppendLine("	hora_leilao = @hora_leilao,                                                     ");
            SQL.AppendLine("	data_hora_cadastro = @data_hora_cadastro,                                       ");
            SQL.AppendLine("	data_agendamento = @data_agendamento,                                           ");
            SQL.AppendLine("	data_inicio_retirada = @data_inicio_retirada,                                   ");
            SQL.AppendLine("	data_fim_retirada = @data_fim_retirada,                                         ");
            SQL.AppendLine("	data_hora_publicacao_diario_oficial = @data_hora_publicacao_diario_oficial,     ");
            SQL.AppendLine("	cep = @cep,                                                                     ");
            SQL.AppendLine("	endereco = @endereco,                                                           ");
            SQL.AppendLine("	end_numero = @end_numero,                                                       ");
            SQL.AppendLine("	end_complemento = @end_complemento,                                             ");
            SQL.AppendLine("	bairro = @bairro,                                                               ");
            SQL.AppendLine("	municipio = @municipio,                                                         ");
            SQL.AppendLine("	uf = @uf,                                                                       ");
            SQL.AppendLine("	id_empresa = @id_empresa,                                                       ");
            SQL.AppendLine("	identificacao_leilao_orgao = @identificacao_leilao_orgao                        ");

            var sqlcmd = new System.Data.SqlClient.SqlCommand();

            System.Data.SqlClient.SqlParameter[] parameters =
             {
                new System.Data.SqlClient.SqlParameter(){ ParameterName = "", Value = "" },
                new System.Data.SqlClient.SqlParameter(){ ParameterName = "", Value = "" },
                new System.Data.SqlClient.SqlParameter(){ ParameterName = "", Value = "" },
                new System.Data.SqlClient.SqlParameter(){ ParameterName = "", Value = "" },
                new System.Data.SqlClient.SqlParameter(){ ParameterName = "", Value = "" },
                new System.Data.SqlClient.SqlParameter(){ ParameterName = "", Value = "" },
                
            };

            return -1;
        }
        
        public int InserirComRetorno(Leilao entidade)
        {            
            //var leiloes = SelecionarTudo().Where(p => p.descricao == entidade.descricao.ToUpper().Trim() || 
            //                                          p.ordem_interna_leilao == entidade.ordem_interna_leilao.ToUpper().Trim() ||
            //                                          p.ordem_interna_matriz == entidade.ordem_interna_matriz.ToUpper().Trim()).FirstOrDefault();


            //if (leiloes != null)
            //{
            //    return -1;
            //}


            StringBuilder sqlIns = new StringBuilder();

            sqlIns.AppendLine(" INSERT INTO dbo.tb_leilao (descricao,                                    ");
            sqlIns.AppendLine("                            nome_local,                                   ");
            sqlIns.AppendLine(" 						   numero_diario_oficial,                        ");
            sqlIns.AppendLine(" 						   ordem_interna_matriz,                         ");
            sqlIns.AppendLine(" 						   ordem_interna_leilao,                         ");
            sqlIns.AppendLine(" 						   id_leiloeiro,                                 ");
            sqlIns.AppendLine(" 						   id_usuario_cadastro,                          ");
            sqlIns.AppendLine(" 						   id_comitente,                                 ");
            sqlIns.AppendLine(" 						   id_expositor,                                 ");
            sqlIns.AppendLine(" 						   id_status,                                    ");
            sqlIns.AppendLine(" 						   data_leilao,                                  ");
            sqlIns.AppendLine(" 						   hora_leilao,                                  ");
            sqlIns.AppendLine(" 						   data_hora_publicacao_diario_oficial,          ");
            sqlIns.AppendLine(" 						   cep,                                          ");
            sqlIns.AppendLine(" 						   endereco,                                     ");
            sqlIns.AppendLine(" 						   end_numero,                                   ");
            sqlIns.AppendLine(" 						   end_complemento,                              ");
            sqlIns.AppendLine(" 						   bairro,                                       ");
            sqlIns.AppendLine(" 						   municipio,                                    ");
            sqlIns.AppendLine(" 						   uf,                                           ");
            sqlIns.AppendLine(" 						   id_empresa,                                   ");
            sqlIns.AppendLine(" 						   id_regra_prestacao_contas)                    ");
            
            sqlIns.AppendLine("VALUES(                                                                   ");

            sqlIns.AppendFormat("                          '{0}'                                         ", entidade.descricao.ToUpper());
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.nome_local.ToUpper());
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.numero_diario_oficial ?? string.Empty);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.ordem_interna_matriz ?? string.Empty);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.ordem_interna_leilao ?? string.Empty);
            sqlIns.AppendFormat("                         , {0}                                          ", entidade.id_leiloeiro);
            sqlIns.AppendFormat("                         , {0}                                          ", entidade.id_usuario_cadastro);
            sqlIns.AppendFormat("                         , {0}                                          ", entidade.id_comitente);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.id_expositor);
            sqlIns.AppendFormat("                         , {0}                                          ", 1);                        //id_status (Inicial)
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.data_leilao);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.hora_leilao);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.data_hora_publicacao_diario_oficial);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.cep == null ? string.Empty : entidade.cep.Replace("-", ""));
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.endereco != null ? entidade.endereco.ToUpper() : string.Empty);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.end_numero ?? string.Empty);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.end_complemento ?? string.Empty);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.bairro ?? string.Empty);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.municipio ?? string.Empty);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.uf ?? string.Empty);
            sqlIns.AppendFormat("                         ,'{0}'                                         ", entidade.id_empresa > 0 ? entidade.id_empresa : - 1);
            sqlIns.AppendFormat("                         , {0})                                         ", entidade.id_regra_prestacao_contas.IsNotNull() ? entidade.id_regra_prestacao_contas : -1);

            return ExecutaSQL_ScopeIdentity(sqlIns.ToString());
        }

        public Leilao SelecionarPorId(int id)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("  SELECT *, (SELECT login                                            ");
            sql.AppendLine("    FROM dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios        ");
            sql.AppendLine("   WHERE id_usuario = tb_leilao.id_usuario_cadastro) Usuario_Cadastro");
            sql.AppendFormat("  FROM tb_leilao WHERE id = {0}", id);
            
            var dtLeilao = ConsultaSQL(sql.ToString());

            if (dtLeilao.Rows.Count > 0)
            {
                var leilao = dtLeilao
                    .Rows[0]
                    .ConverterParaEntidade<Leilao>();

                if (leilao != null)
                {
                    var comitente = new ComitenteRepositorio().SelecionarPorId(leilao.id_comitente);

                    if (comitente.id_cliente != null)
                    {
                        var depositos = new DepositoRepositorio().SelecionarTudoPorCliente(comitente.id_cliente.Value);
                        leilao.depositos = depositos.ToList();
                    }

                    leilao.comitente = comitente;

                    return leilao;
                }
                else
                {
                    return null;
                }
                
            }
            else
            {
                return null;
            }
        }

        public IList<Leilao> SelecionarTudo()
        {
            string sql = @"
                 SELECT tb_leilao.*, tb_comitentes.descricao Nome_Comitente, 					   
					   (SELECT COUNT(*) 
				          FROM tb_leilao_lotes
				         WHERE id_leilao = tb_leilao.id) AS quantidade_lotes, 

                       (SELECT COUNT(*) 
				          FROM tb_leilao_lotes
				         WHERE id_leilao = tb_leilao.id
				           AND id_status_lote IN (SELECT id FROM tb_lotes_status WHERE situacao_lote = 'S')) AS quantidade_lotes_validos, 
				       
				       dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios.login usuario_cadastro,
				       
				       (SELECT COUNT(*) FROM tb_leilao_lotes_arrematantes WHERE leilao = tb_leilao.descricao) qtd_arrematantes,
				         
				         (SELECT COUNT(*) FROM tb_leilao_despesas WHERE id_leilao = tb_leilao.id) qtd_despesas  

                  FROM tb_leilao
            INNER JOIN tb_comitentes ON tb_comitentes.id = tb_leilao.id_comitente
            INNER JOIN tb_leilao_status ON tb_leilao_status.id = tb_leilao.id_status 
            
            LEFT JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios 
                   ON tb_leilao.id_usuario_cadastro = dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios.id_usuario
                WHERE tb_leilao_status.ativo = 'A'";

            List<Leilao> lista = ConsultaSQL(sql).ConverterParaLista<Leilao>();

            StatusLeilaoRepositorio StatusLeilaoRepositorio = new StatusLeilaoRepositorio();

            foreach (var item in lista)
            {
                item.status = StatusLeilaoRepositorio.SelecionarPorId(item.id_status);
                //item.
            }

            return lista;
        }

        public IList<Leilao> SelecionarTudo(RepositorioGlobal.Status Status = RepositorioGlobal.Status.Ativo)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("	 SELECT tb_leilao.*, tb_comitentes.descricao Nome_Comitente, 					                                       ");
            sql.AppendLine("		   (SELECT COUNT(*)                                                                                                ");
            sql.AppendLine("			  FROM tb_leilao_lotes                                                                                         ");
            sql.AppendLine("			 WHERE id_leilao = tb_leilao.id) AS quantidade_lotes,                                                          ");
            sql.AppendLine("                                                                                                                           ");
            sql.AppendLine("		   (SELECT COUNT(*)                                                                                                ");
            sql.AppendLine("			  FROM tb_leilao_lotes                                                                                         ");
            sql.AppendLine("			 WHERE id_leilao = tb_leilao.id                                                                                ");
            sql.AppendLine("			   AND id_status_lote IN (SELECT id FROM tb_lotes_status WHERE situacao_lote = 'S')) AS quantidade_lotes_validos, ");
            sql.AppendLine("		                                                                                                                   ");
            sql.AppendLine("		   dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios.login usuario_cadastro,                                    ");
            sql.AppendLine("		                                                                                                                   ");
            sql.AppendLine("		   (SELECT COUNT(*) FROM tb_leilao_lotes_arrematantes WHERE leilao = tb_leilao.descricao) qtd_arrematantes,        ");
            sql.AppendLine("			                                                                                                               ");
            sql.AppendLine("			 (SELECT COUNT(*) FROM tb_leilao_despesas WHERE id_leilao = tb_leilao.id) qtd_despesas                         ");
            //sql.AppendLine("			 (SELECT COUNT(*) FROM tb_leilao_notificacoes WHERE id_leilao = tb_leilao.id) qtd_notificacoes                 ");
            sql.AppendLine("                                                                                                                           ");
            sql.AppendLine("	  FROM tb_leilao                                                                                                       ");
            sql.AppendLine("INNER JOIN tb_comitentes ON tb_comitentes.id = tb_leilao.id_comitente                                                      ");
            sql.AppendLine("INNER JOIN tb_leilao_status ON tb_leilao_status.id = tb_leilao.id_status                                                   ");
            sql.AppendLine("                                                                                                                           ");
            sql.AppendLine("LEFT JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios                                                             ");
            sql.AppendLine("	   ON tb_leilao.id_usuario_cadastro = dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios.id_usuario                  ");
            sql.AppendLine(string.Format("	WHERE tb_leilao_status.ativo = '{0}'                                   ",  Status.ToString().Substring(0,1)));

            List<Leilao> lista = ConsultaSQL(sql.ToString()).ConverterParaLista<Leilao>();

            StatusLeilaoRepositorio StatusLeilaoRepositorio = new StatusLeilaoRepositorio();

            foreach (var item in lista)
            {
                item.status = StatusLeilaoRepositorio.SelecionarPorId(item.id_status);
                //item.
            }

            return lista;
        }

        public IList<Leilao> SelecionarTudoDETRO()
        {
            string sql = @"SELECT tb_leilao.* 
              FROM tb_leilao 
              INNER JOIN tb_comitentes ON tb_comitentes.id = tb_leilao.id_comitente
              WHERE tipo_importacao = 1 ";
                 
            List<Leilao> lista = ConsultaSQL(sql).ConverterParaLista<Leilao>();

            StatusLeilaoRepositorio StatusLeilaoRepositorio = new StatusLeilaoRepositorio();

            foreach (var item in lista)
            {
                item.status = StatusLeilaoRepositorio.SelecionarPorId(item.id_status);
            }

            return lista;
        }

        public IList<StatusLeilao> StatusDeLeilao()
        {
            string sql = @"SELECT id, descricao FROM tb_leilao_status";

            var lista = ConsultaSQL(sql).ConverterParaLista<StatusLeilao>();

            return lista;
        }

        public int AlterarStatus(int IdLeilao, int Status)
        {
            string sql = 
                string.Format(@"  update tb_leilao set id_status = {0} where id = {1} ", Status, IdLeilao);

            return ExecutaSQL(sql);
        }

        public IList<Leilao> SelecionarTudo(Leilao Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Leilao> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Leilao Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Leilao Entidade)
        {
            throw new NotImplementedException();
        }
    }
}
