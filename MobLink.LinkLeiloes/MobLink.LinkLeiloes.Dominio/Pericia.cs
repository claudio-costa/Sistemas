using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{
    public class Pericia
    {
        public Pericia()
        {
            //StatusPericia = new StatusPericia();
            //StatusVeiculo = new StatusVeiculoPericia();
        }

        public DateTime? DataPericia { get; set; }
        public string NumeroLaudo { get; set; }
        public int StatusPericia { get; set; }
        public int StatusVeiculo { get; set; }
        public string Observacoes { get; set; }
    }

    public class ArquivoPericia
    {
        public int Id { get; set; }
        public int Id_Lote { get; set; }
        public string Nome { get; set; }
        public int Tamanho { get; set; }
        public string Tipo { get; set; }
        public string Path { get; set; }
        public string Usuario { get; set; }
    }
}
