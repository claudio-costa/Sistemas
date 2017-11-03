using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;
using System.Text;


namespace MobLink.LinkLeiloes.Repositorio
{
    public class PericiaRepositorio : DbSqlServer, ICrud<Pericia, int>
    {
        protected internal PericiaRepositorio(): base(Util.DetectarConexao())
        {

        }

        public List<StatusPericia> ListaStatus()
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT * FROM tb_pericia_status");

            return ConsultaSQL(sql.ToString()).ConverterParaLista<StatusPericia>();
        }

        public List<StatusVeiculoPericia> ListaStatusVeiculos()
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT * FROM tb_pericia_status_veiculo");

            return ConsultaSQL(sql.ToString()).ConverterParaLista<StatusVeiculoPericia>();
        }

        public List<ArquivoPericia> SelecionarArquivos(int id)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("select * from tb_leilao_lotes_pericia_arquivos");
            SQL.AppendFormat("where id_lote = {0}", id);

            return ConsultaSQL(SQL.ToString()).ConverterParaLista<ArquivoPericia>();
        }

        public ArquivoPericia SelecionarArquivosPorID(int id)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("select * from tb_leilao_lotes_pericia_arquivos");
            SQL.AppendFormat("where id = {0}", id);

            var dtConsulta = ConsultaSQL(SQL.ToString());

            if (dtConsulta.Rows.Count > 0)
            {
                return dtConsulta.Rows[0].ConverterParaEntidade<ArquivoPericia>();
            }
            else
            {
                return new ArquivoPericia();
            }
        }

        public int InserirArquivo(ArquivoPericia ArquivoPericia)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("INSERT INTO dbo.tb_leilao_lotes_pericia_arquivos (id_lote, nome, tamanho, tipo, path, usuario)   ");
            SQL.AppendFormat("VALUES ({0}, '{1}', {2}, '{3}', '{4}', '{5}')", ArquivoPericia.Id_Lote,
                                                                              ArquivoPericia.Nome,
                                                                              ArquivoPericia.Tamanho,
                                                                              ArquivoPericia.Tipo,
                                                                              ArquivoPericia.Path,
                                                                              ArquivoPericia.Usuario);
            try
            {
                return ExecutaSQL(SQL.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int ExcluirArquivo(int id)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendFormat("DELETE tb_leilao_lotes_pericia_arquivos WHERE id = {0}", id);

            try
            {
                return ExecutaSQL(SQL.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IList<Pericia> SelecionarTudo()
        {
            throw new NotImplementedException();
        }

        public IList<Pericia> SelecionarTudo(Pericia Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Pericia> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }

        public Pericia SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Pericia Entidade)
        {
            throw new NotImplementedException();
        }

        public int Alterar(Pericia Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Pericia Entidade)
        {
            throw new NotImplementedException();
        }
    }
}
