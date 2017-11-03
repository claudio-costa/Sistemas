using System;
using System.Data;
using System.Diagnostics;

namespace MobLink.Framework.DAL
{
    public class SqlServer
    {
        private string _ConnectionString = string.Empty;
        private Comum.Global.Ambientes.TiposAmbiente _Ambiente;
        private Comum.Global.Sistema _Sistema;

        protected internal Comum.Global.Ambientes.TiposAmbiente Ambiente { get { return _Ambiente; } }
        protected internal Comum.Global.Sistema Sistema { get { return _Sistema; } }
        protected internal string ConnectionString { get { return _ConnectionString; } }

        protected SqlServer(Comum.Global.Sistema Sistema, Comum.Global.Ambientes.TiposAmbiente Ambiente)
        {
            _Ambiente = Ambiente;
            _Sistema = Sistema;

            switch (Sistema)
            {
                case Comum.Global.Sistema.DepositoPublico:
                    MSDAL.ConnectionFactory.connectionString = Comum.Parametros.ConnectionStrings.dbMobLinkDepositoPublico(Ambiente);
                    _ConnectionString = MSDAL.ConnectionFactory.connectionString;
                    break;
                case Comum.Global.Sistema.Leilao:
                    MSDAL.ConnectionFactory.connectionString = Comum.Parametros.ConnectionStrings.dbMobLinkLeilao(Ambiente);
                    _ConnectionString = MSDAL.ConnectionFactory.connectionString;
                    break;
                case Comum.Global.Sistema.Sap:
                    MSDAL.ConnectionFactory.connectionString = Comum.Parametros.ConnectionStrings.dbMobLinkSap(Ambiente);
                    _ConnectionString = MSDAL.ConnectionFactory.connectionString;
                    break;
                default:
                    break;
            }
        }

        #region OPERAÇÕES
        protected DataTable ConsultaSQL(string sql)
        {
            try
            {
                MSDAL.ConnectionFactory.ConnectDataBase();

                return MSDAL.ConnectionFactory.Consultar(sql) ?? new DataTable();
            }
            catch (Exception e)
            {
                MSDAL.ConnectionFactory.DisconnectDataBase();
                Debug.Print("Erro ao executar consulta:\n" + e.ToString());
                throw e;
            }
            finally
            {
                MSDAL.ConnectionFactory.DisconnectDataBase();
            }
        }

        protected int ExecutaSQL(string sql)
        {
            try
            {
                MSDAL.ConnectionFactory.ConnectDataBase();

                return MSDAL.ConnectionFactory.Execute(sql);
            }
            catch (Exception e)
            {
                MSDAL.ConnectionFactory.DisconnectDataBase();
                Debug.Print("Erro ao executar sql:\n" + e.ToString());
                Debug.Print(sql);
                throw e;
            }
            finally
            {
                MSDAL.ConnectionFactory.DisconnectDataBase();
            }
        }

        protected int ExecutaSQL_ScopeIdentity(string sql)
        {
            try
            {
                MSDAL.ConnectionFactory.ConnectDataBase();

                return MSDAL.ConnectionFactory.ExecuteScopeIdentity(sql);
            }
            catch (Exception e)
            {
                MSDAL.ConnectionFactory.DisconnectDataBase();
                Debug.Print("Erro ao executar sql:\n" + e.ToString());
                throw e;
            }
            finally
            {
                MSDAL.ConnectionFactory.DisconnectDataBase();
            }
        }
        #endregion
    }
}
