using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;
using System.Text;


namespace MobLink.WebLeilao.Repositorio
{
    public class FinanceiraRepositorio : DbSqlServer, ICrud<Financeira, int>
    {
        protected internal FinanceiraRepositorio() : base(Util.DetectarConexao())
        {

        }

        public int Alterar(Financeira Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Financeira Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Financeira Entidade)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(string.Format("            INSERT INTO dbo.tb_financeiras     "));
            sql.AppendLine(string.Format("            		(cnpj                        "));
            sql.AppendLine(string.Format("            		, nome                       "));
            sql.AppendLine(string.Format("            		, segmento                   "));
            sql.AppendLine(string.Format("            		, endereco                   "));
            sql.AppendLine(string.Format("            		, complemento                "));
            sql.AppendLine(string.Format("            		, bairro                     "));
            sql.AppendLine(string.Format("            		, cep                        "));
            sql.AppendLine(string.Format("            		, municipio                  "));
            sql.AppendLine(string.Format("            		, uf                         "));
            sql.AppendLine(string.Format("            		, ddd                        "));
            sql.AppendLine(string.Format("            		, fone                       "));
            sql.AppendLine(string.Format("            		, email                      "));
            sql.AppendLine(string.Format("            		, site)                      "));
            sql.AppendLine(string.Format("            		                             "));
            sql.AppendLine(string.Format("            VALUES                             "));
            sql.AppendLine(string.Format("            		( '{0}'                      ", Entidade.cnpj.Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", Entidade.nome.Replace("'", "").Trim().ToUpper()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", Entidade.segmento.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", Entidade.endereco.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", Entidade.complemento.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", Entidade.bairro.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", Entidade.cep.Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", Entidade.municipio.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", Entidade.uf.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", Entidade.ddd.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", Entidade.fone.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", Entidade.email.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}')                     ", Entidade.site.ToUpper().Trim()));

            return ExecutaSQL(sql.ToString());
        }

        public Financeira SelecionarPorId(int id)
        {
            string sql = "SELECT * FROM dbo.tb_financeiras where id = " + id;
            return ConsultaSQL(sql).Rows[0].ConverterParaEntidade<Financeira>();
        }

        public IList<Financeira> SelecionarTudo()
        {
            return ConsultaSQL("select * from tb_financeiras").ConverterParaLista<Financeira>();
        }

        public IList<Financeira> SelecionarTudo(Financeira Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Financeira> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
