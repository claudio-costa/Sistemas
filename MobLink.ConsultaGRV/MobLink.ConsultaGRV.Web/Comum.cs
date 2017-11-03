using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace MobLink.ConsultaGRV.Web
{
    public class Comum
    {
        public static string ProcedimentoLiberacaoUrlHtml(int id_cliente)
        {
            return string.Format("http://www.linkpatios.com.br/mobile/{0}.html", id_cliente.ToString());
        }

        
    }
}