using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobLink.Dashboard
{
    public class Suporte
    {
        public static Dictionary<object, object> SelecionaColunasGraficoPizza()
        {
            Dictionary<object, object> dic = new Dictionary<object, object>();

            dic.Add("string", "Região");
            dic.Add("number", "Quantidade");

            return dic;
        }

        public static Dictionary<object, object> SelecionaLinhasGraficoPizza()
        {
            Dictionary<object, object> dic = new Dictionary<object, object>();

            dic.Add("Zona Norte", 3);
            dic.Add("Zona Sul", 7);
            dic.Add("Zona Oeste", 2);
            dic.Add("Zona Leste", 14);
            dic.Add("Outras", 6);

            return dic;
        }
    }
}