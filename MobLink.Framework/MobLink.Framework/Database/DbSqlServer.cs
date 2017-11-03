using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.Framework.Database
{
    public class DbSqlServer : Db
    {
        protected DbSqlServer(string connection_string)
        {
            ConnectionString = connection_string;
        }

        protected override DataTable ConsultaSQL(string sql)
        {
            using (var conexao = new SqlConnection(ConnectionString))
            {
                try
                {
                    conexao.Open();

                    SqlCommand sqlCommand = new SqlCommand(sql, conexao);

                    var dt = new DataTable();

                    var dr = sqlCommand.ExecuteReader();

                    dt.Load(dr);

                    return dt;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        protected override List<T> ConsultaSQL<T>(string sql)
        {
            throw new NotImplementedException();
        }

        protected override int ExecutaSQL(string sql)
        {
            using (var conexao = new SqlConnection(ConnectionString))
            {
                try
                {
                    conexao.Open();

                    SqlCommand sqlCommand = new SqlCommand(sql, conexao);

                    var dt = new DataTable();

                    return sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        protected override int ExecutaSQL_ScopeIdentity(string sql)
        {
            using (var conexao = new SqlConnection(ConnectionString))
            {
                try
                {
                    conexao.Open();

                    SqlCommand sqlCommand = new SqlCommand(sql + ";SELECT SCOPE_IDENTITY();", conexao);

                    var id = sqlCommand.ExecuteScalar();

                    return System.Convert.ToInt32(id);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conexao.Close();
                }
            }
        }


        //public DbSqlServer(string ConnectionString) : base()
        //{
        //    MSDAL.ConnectionFactory.connectionString = ConnectionString;
        //}

        //public string AlterarConexao(string ConnectionString)
        //{
        //    MSDAL.ConnectionFactory.connectionString = ConnectionString;

        //    return "OK";
        //}

        //protected override DataTable ConsultaSQL(string sql)
        //{
        //    try
        //    {
        //        MSDAL.ConnectionFactory.ConnectDataBase();

        //        return MSDAL.ConnectionFactory.Consultar(sql) ?? new DataTable();
        //    }
        //    catch (Exception e)
        //    {
        //        MSDAL.ConnectionFactory.DisconnectDataBase();
        //        throw e;
        //    }
        //    finally
        //    {
        //        MSDAL.ConnectionFactory.DisconnectDataBase();
        //    }
        //}

        //protected override int ExecutaSQL(string sql)
        //{
        //    try
        //    {
        //        MSDAL.ConnectionFactory.ConnectDataBase();

        //        return MSDAL.ConnectionFactory.Execute(sql);
        //    }
        //    catch (Exception e)
        //    {
        //        MSDAL.ConnectionFactory.DisconnectDataBase();
        //        throw e;
        //    }
        //    finally
        //    {
        //        MSDAL.ConnectionFactory.DisconnectDataBase();
        //    }
        //}

        //protected override int ExecutaSQL_ScopeIdentity(string sql)
        //{
        //    try
        //    {
        //        MSDAL.ConnectionFactory.ConnectDataBase();

        //        return MSDAL.ConnectionFactory.ExecuteScopeIdentity(sql);
        //    }
        //    catch (Exception e)
        //    {
        //        MSDAL.ConnectionFactory.DisconnectDataBase();
        //        throw e;
        //    }
        //    finally
        //    {
        //        MSDAL.ConnectionFactory.DisconnectDataBase();
        //    }
        //}

        //protected List<DbInstancia> RecuperaListaInstancias()
        //{
        //    return ConsultaSQL("select name nome, database_id id from sys.databases").ConverterParaLista<DbInstancia>();
        //}
    }
}
