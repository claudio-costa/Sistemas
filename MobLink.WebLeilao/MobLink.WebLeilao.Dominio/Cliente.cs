using System.Collections.Generic;

namespace MobLink.WebLeilao.Dominio
{
    public class Cliente
    {
        public Cliente()
        {
            depositos = new List<Deposito>();
        }
        
        public int id { get; set; }
        public string nome { get; set; }
        public string endereco { get; set; }
        
        public virtual List<Deposito> depositos { get; set; }
    }
}
