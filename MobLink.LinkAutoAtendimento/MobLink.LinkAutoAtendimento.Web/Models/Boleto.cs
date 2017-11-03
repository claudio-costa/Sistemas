using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobLink.ConsultaGRV.Web.Models
{
    public class Boleto
    {
        public byte[] ImagemBoleto { get; set; }
        public int IdBoleto { get; set; }
        public string LinhaDigitavel { get; set; }
        public string Vencimento { get; set; }
        public string Valor { get; set; }
        public int IdGRV { get; set; }
    }
}