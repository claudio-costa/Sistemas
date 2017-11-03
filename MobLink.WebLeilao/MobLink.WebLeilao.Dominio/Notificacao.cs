using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{
    public class NotificacaoTipos : TabelaGenerica
    {
        public string flg_ativo { get; set; }
    }

    public class Notificacao
    {
        public int id { get; set; }
        public int id_notificacao_tipo { get; set; }
        public int id_cliente { get; set; }
        public int id_deposito { get; set; }
        public int id_usuario { get; set; }
        public DateTime data_hora { get; set; }
    }
}
