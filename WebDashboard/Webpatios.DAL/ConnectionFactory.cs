using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WebPatios.DAL
{
    public static class ConnectionFactory
    {
        public static string connectionString;

        private static DataTable ObterTabela(SqlDataReader reader)
        {
            DataTable tbEsquema = reader.GetSchemaTable();
            DataTable tbRetorno = new DataTable();

            foreach (DataRow r in tbEsquema.Rows)
            {
                if (!tbRetorno.Columns.Contains(r["ColumnName"].ToString()))
                {
                    DataColumn col = new DataColumn()
                    {
                        ColumnName = r["ColumnName"].ToString(),
                        Unique = Convert.ToBoolean(r["IsUnique"]),
                        AllowDBNull = Convert.ToBoolean(r["AllowDBNull"]),
                        ReadOnly = Convert.ToBoolean(r["IsReadOnly"])
                    };

                    tbRetorno.Columns.Add(col);
                }
            }

            while (reader.Read())
            {
                DataRow novaLinha = tbRetorno.NewRow();

                for (int i = 0; i < tbRetorno.Columns.Count; i++)
                {
                    novaLinha[i] = reader.GetValue(i);
                }

                tbRetorno.Rows.Add(novaLinha);

            }

            return tbRetorno;
        }

        public static DataTable Consulta(string SQL)
        {
            ConnectionFactory.connectionString = @"Server = 179.107.47.90; Database = dbMobLinkDepositoPublicoProducao; User Id = sapdev; Password = Buracica#;";

            if (string.IsNullOrEmpty(connectionString))
            {
                return new DataTable();
            }

            DataTable tbRes = new DataTable();

            SqlConnection _SqlConnection = new SqlConnection();
            _SqlConnection.ConnectionString = connectionString;
            SqlCommand _SqlCommand = new SqlCommand(SQL);
            _SqlCommand.Connection = _SqlConnection;

            try
            {
                _SqlConnection.Open();
                tbRes.Load(_SqlCommand.ExecuteReader());
                return tbRes;
            }
            catch
            {
                return new DataTable();
            }
            finally
            {
                _SqlConnection.Close();
            }

        }

        public static int Executar(string SQL)
        {
            //ConnectionFactory.connectionString = @"Server = 179.107.47.90; Database = dbMobLinkSAP; User Id = sapdev; Password = Buracica#;";

            if (string.IsNullOrEmpty(connectionString))
            {
                return -1;
            }

            SqlConnection _SqlConnection = new SqlConnection();
            _SqlConnection.ConnectionString = connectionString;
            SqlCommand _SqlCommand = new SqlCommand(SQL);
            _SqlCommand.Connection = _SqlConnection;

            try
            {
                _SqlConnection.Open();
                return _SqlCommand.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }
            finally
            {
                _SqlConnection.Close();
            }
            
        }

        public static object ExecutarEscalar(string SQL)
        {
            //ConnectionFactory.connectionString = @"Server = 179.107.47.90; Database = dbMobLinkSAP; User Id = sapdev; Password = Buracica#;";

            if (string.IsNullOrEmpty(connectionString))
            {
                return -1;
            }

            SqlConnection _SqlConnection = new SqlConnection();
            _SqlConnection.ConnectionString = connectionString;
            SqlCommand _SqlCommand = new SqlCommand(SQL);
            _SqlCommand.Connection = _SqlConnection;

            try
            {
                _SqlConnection.Open();
                return _SqlCommand.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
            finally
            {
                _SqlConnection.Close();
            }

        }
    }
}
