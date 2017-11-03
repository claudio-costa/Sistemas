using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{
    public class NotificacaoLeilao : Notificacao
    {
        public int id_notificacao { get; set; }
        public int id_leilao { get; set; }
        public int id_usuario { get; set; }
        public DateTime data_geracao { get; set; }
        public string descricao_leilao { get; set; }
        public string login { get; set; }
    }
}
