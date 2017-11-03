using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace MobLink.Framework.Comum.AdoNet
{
    public class RepositorioSqlServer__
    {
        private Dictionary<int, string> DicionarioConexoes = new Dictionary<int, string>();
        private ConexoesProntas _ConexaoEscolhida;
        private string ConnectionString;       

        protected List<string> ComandoSQL;

        protected RepositorioSqlServer__(string StringConexao)
        {
            ConnectionString = StringConexao;
        }

        protected RepositorioSqlServer__(ConexoesProntas Conexao)
        {
            _ConexaoEscolhida = Conexao;

            ComandoSQL = new List<string>();

            DicionarioConexoes.Add(0, @"Server=179.107.47.90;
                                        Database=dbLeilao;
                                        User Id=linkpatio;
                                        Password=LinkPatio2016+;");


            DicionarioConexoes.Add(1, @"Server=179.107.47.90;
                                                    Database=dbMobLinkDepositoPublicoProducao;
                                                    User Id=sapdev;
                                                    Password = Buracica#;");
        }

        private SqlConnection Conexao()
        {
            var cs = DicionarioConexoes.FirstOrDefault(con => con.Key == (int)_ConexaoEscolhida).Value;
            return new SqlConnection(cs);
        }

        protected enum ConexoesProntas
        {
            dbLeilao = 0
            //dbLeilao = 0, dbMobLinkDepositoPublicoProducao = 1
        }

        protected DataTable RetornarConsulta()
        {
            using (SqlConnection conn = Conexao())
            {
                DataTable tbResultado = new DataTable();

                try
                {
                    SqlCommand cmd = new SqlCommand(ComandoSQL.ExtrairStr(), conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    tbResultado.Load(dr);
                    return tbResultado;
                }
                catch(Exception e)
                {
                    conn.Close();
                    ComandoSQL.Clear();                    
                    Debug.Print("Erro ao executar consulta:\n" + e.ToString());
                    throw e;
                    //return new DataTable();
                }
                finally
                {   
                    conn.Close();
                    ComandoSQL.Clear();
                }
                
            }
            
        }

        protected int ExecutarComandoRetornoID()
        {
            using (SqlConnection conn = Conexao())
            {
                foreach (var item in ComandoSQL)
                {

                }

                try
                {
                    SqlCommand cmd = new SqlCommand(ComandoSQL.ExtrairStr(), conn);
                    cmd.CommandText += " SELECT SCOPE_IDENTITY() ";
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception e)
                {
                    conn.Close();
                    ComandoSQL.Clear();
                    Debug.Print("Erro ao executar comando:\n" + e.ToString());
                    throw e;
                }
                finally
                {
                    conn.Close();
                    ComandoSQL.Clear();
                }

            }
        }

        protected void ExecutarComando()
        {
            using (SqlConnection conn = Conexao())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(ComandoSQL.ExtrairStr(), conn);
                    conn.Open();
                    cmd.ExecuteScalar();
                }
                catch (Exception e)
                {
                    conn.Close();
                    ComandoSQL.Clear();
                    Debug.Print("Erro ao executar comando:\n" + e.ToString());
                    throw e;
                }
                finally
                {
                    conn.Close();
                    ComandoSQL.Clear();
                }

            }
        }

        protected void ExecutarComando(SqlParameter[] Parametros)
        {
            using (SqlConnection conn = Conexao())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(ComandoSQL.ExtrairStr(), conn);


                    cmd.Parameters.AddRange(Parametros);

                    conn.Open();
                    cmd.ExecuteScalar();
                }
                catch (Exception e)
                {
                    conn.Close();
                    ComandoSQL.Clear();
                    Debug.Print("Erro ao executar comando:\n" + e.ToString());
                    throw e;
                }
                finally
                {
                    conn.Close();
                    ComandoSQL.Clear();
                }

            }
        }
    }

}