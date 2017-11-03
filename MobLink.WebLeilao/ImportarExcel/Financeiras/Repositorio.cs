using MobLink.Framework;
using MobLink.Framework.Database;
using System.Text;

namespace ImportarExcel
{
    public class Repositorio : DbSqlServer
    {
        public Repositorio() : base(Util.DetectarConexao())
        {

        }

        public void InserirFinanceira(Financeira f)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(string.Format("            INSERT INTO dbo.tb_financeiras     "));
            sql.AppendLine(string.Format("            		(cnpj                        "));
            sql.AppendLine(string.Format("            		, nome                       "));
            //sql.AppendLine(string.Format("            		, segmento                   "));
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
            sql.AppendLine(string.Format("            		( '{0}'                      ", f.cnpj.Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", f.nome.Replace("'","").Trim().ToUpper()));
            //sql.AppendLine(string.Format("            		, '{0}'                      ", f.segmento.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", f.endereco.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", f.complemento.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", f.bairro.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", f.cep.Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", f.municipio.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", f.uf.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", f.ddd.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", f.fone.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}'                      ", f.email.ToUpper().Trim()));
            sql.AppendLine(string.Format("            		, '{0}')                     ", f.site.ToUpper().Trim()));

            ExecutaSQL(sql.ToString());
        }
    }
}
