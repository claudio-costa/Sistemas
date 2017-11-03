using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;


namespace MobLink.LinkLeiloes.Repositorio
{
    public class DepositoRepositorio : DbSqlServer, ICrud<Deposito, int>
    {
        protected internal DepositoRepositorio() : base(Util.LerConfiguracao("CONEXAO_DP"))
        {

        }

        public int Alterar(Deposito Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Deposito Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Deposito Entidade)
        {
            throw new NotImplementedException();
        }

        public Deposito SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Deposito> SelecionarTudo()
        {
            return ConsultaSQL(@"
               SELECT DEP.id_deposito, CLIDEP.id_cliente, DEP.descricao
              FROM dbo.tb_dep_clientes_depositos CLIDEP
        INNER JOIN dbo.tb_dep_depositos DEP ON CLIDEP.id_deposito = DEP.id_deposito
             WHERE DEP.flag_ativo = 'S' ").ConverterParaLista<Deposito>();
        }

        public DepositoSimplificado SelecionarNomeEndereco(int IdCliente)
        {
            string sql = string.Format(@"
            SELECT id_cliente, nome, cnpj, endereco_completo, flag_ativo 
              FROM vw_dep_clientes 
             WHERE id_cliente = {0}", IdCliente);

            var dt = ConsultaSQL(sql);

            return dt.Rows.Count == 0 ? new DepositoSimplificado() : dt.Rows[0].ConverterParaEntidade<DepositoSimplificado>();
        }

        public IList<Deposito> SelecionarTudo(Deposito Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Deposito> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }
        
        public IList<Deposito> SelecionarTudoPorCliente(int IdCliente)
        {
            return ConsultaSQL(@"
               SELECT DEP.id_deposito, CLIDEP.id_cliente, DEP.descricao, DEP.id_sistema_externo
              FROM dbo.tb_dep_clientes_depositos CLIDEP
        INNER JOIN dbo.tb_dep_depositos DEP ON CLIDEP.id_deposito = DEP.id_deposito
             WHERE DEP.flag_ativo = 'S' 
               AND CLIDEP.id_cliente = " + IdCliente).ConverterParaLista<Deposito>();
        }
    }
}
