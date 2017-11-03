using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.LinkLeiloes.Dominio
{
    public class Leiloeiro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? Data_Hora_Cadastro { get; set; }
        public int Comissao { get; set; }
        public string Orgao_Vinculado { get; set; }
        public int Id_Usuario_Cadastro { get; set; }

        public virtual string NomeUsuarioCadastro { get; set; }
    }
}
