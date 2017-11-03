using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPatios.Model
{
    public class Usuario
    {
        public Usuario()
        {
            depositosUsuario = new List<Deposito>();
            clientesUsuario = new List<Cliente>();
        }

        public int idUsuario { get; set; }
        public string loginUsuario { get; set; }
        public string emailUsuario { get; set; }
        public string nomeUsuario { get; set; }
        public string ipUsuario { get; set; }

        public IList<Deposito> depositosUsuario { get; set; }
        public IList<Cliente> clientesUsuario { get; set; }
    }
}
