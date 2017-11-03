using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelatórioPRF
{
    public class Relatorio
    {
        public string numero_formulario_grv { get; set; }
        public string placa { get; set; }
        public string chassi { get; set; }
        public string marca_modelo { get; set; }
        public string tipo_veiculo { get; set; }
        public string cor { get; set; }
        public string flag_comboio { get; set; }
        public string data_hora_remocao { get; set; }
        public string data_hora_guarda { get; set; }
        public string status { get; set; }
        public string id_grv { get; set; }
        public string id_tarifa_tipo_veiculo { get; set; }
        public string id_cliente { get; set; }
        public string id_deposito { get; set; }
        public string id_reboquista { get; set; }
        public string id_reboque { get; set; }
        public string id_autoridade_responsavel { get; set; }
        public string id_cor { get; set; }
        public string id_detran_marca_modelo { get; set; }
        public string data_cadastro { get; set; }
        public string ValorTotalAteHoje { get; set; }
    }
}