using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobLink.Framework.Database;
using MobLink.Framework;

namespace MobLink.ConsultaGRV.Web.Repositorio
{
    public class PRFRepositorio : DbSqlServer
    {
        public PRFRepositorio() : base(MobLink.Framework.Util.LerConfiguracao("CONEXAO"))
        {
            
        }

        public List<Models.Relatorio> DadosRelatorioPRF(string cliente, string deposito)
        {
            return ConsultaSQL(string.Format("SELECT * FROM dbLeilao.dbo.tb_relatorio_prf where id_cliente = {0} and id_deposito = {1}", cliente, deposito)).ConverterParaLista<Models.Relatorio>();
        }

        public List<Models.TabelaGenerica> ClientesPRF()
        {
            return ConsultaSQL("SELECT id_cliente id, nome descricao FROM dbMobLinkDepositoPublicoProducao.dbo.vw_dep_clientes WHERE id_cliente IN (13,14)")
                .ConverterParaLista<Models.TabelaGenerica>();
        }

        public List<Models.TabelaGenerica> DepositosPRF()
        {
            return ConsultaSQL("SELECT id_deposito id, deposito_nome descricao FROM dbMobLinkDepositoPublicoProducao.dbo.vw_dep_clientes_depositos WHERE id_cliente IN (13,14) order by id_cliente")
                .ConverterParaLista<Models.TabelaGenerica>();
        }
    }
}