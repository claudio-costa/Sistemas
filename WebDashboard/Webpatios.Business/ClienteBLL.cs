using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPatios.Business
{
    public class ClienteBLL : BaseBLL
    {
        public Model.Cliente getDadosCliente()
        {

            //    select cli.nome, dep.descricao
            //      from tb_dep_clientes cli
            //      inner join tb_dep_clientes_depositos cdp on cli.id_cliente = cdp.id_cliente
            //      inner join tb_dep_depositos dep on cdp.id_deposito = dep.id_deposito
            //    where cli.id_cliente = 5
            //    AND cdp.id_deposito = 3

            var dep = new Model.Deposito() { descricaoDeposito = "ITABUNA", idDeposito = 1 };
            List<Model.Deposito> deps = new List<Model.Deposito>();
            deps.Add(dep);
            return new Model.Cliente() { idCliente = 1, nomeCliente = "PREFEITURA DE ITABUNA" };
        }
    }
}
