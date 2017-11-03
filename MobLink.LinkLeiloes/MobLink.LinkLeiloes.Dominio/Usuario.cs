using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.LinkLeiloes.Dominio
{
    public class PerfilAcesso
    {
        public int Id_Perfil_Acesso { get; set; }
        public int Id_Usuario { get; set; }
        public string Descricao { get; set; }
        public DateTime? Data_Cadastro { get; set; }
        public string Flag_Ativo { get; set; }
    }

    public class Modulo
    {
        public int Id_Modulo { get; set; }
        public int Id_Sub_Modulo { get; set; }
        public string DescricaoModulo { get; set; }
        public string Menu { get; set; }
        public string SubMenu { get; set; }
        public string Formulario { get; set; }
        public string Descricao { get; set; }
    }

    public class Usuario
    {
        public Usuario()
        {
            PerfisAcesso = new List<PerfilAcesso>();
            Modulos = new List<Modulo>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string NomeCompleto { get; set; }
        public string DataCadastro { get; set; }

        public virtual List<PerfilAcesso> PerfisAcesso { get; set; }
        public virtual List<Modulo> Modulos { get; set; }
    }
}
