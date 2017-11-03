using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{
    public class DepositoSimplificado
    {
        public int id_cliente { get; set; }
        public string nome { get; set; }
        public string cnpj { get; set; }
        public string endereco_completo { get; set; }
        public string flag_ativo { get; set; }
    }

    public class Deposito
    {
        public Deposito()
        {
            Cliente = new Cliente();
        }

        public int Id_Deposito { get; set; }
        public int Id_Cliente { get; set; }
        public string Descricao { get; set; }
        public int? Id_Sistema_Externo { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
