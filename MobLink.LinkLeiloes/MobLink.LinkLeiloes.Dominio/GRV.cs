using System;

namespace MobLink.LinkLeiloes.Dominio
{
    public class GRV
    {
        public GRV()
        {
            cliente = new Cliente();
            deposito = new Deposito();
        }

        public int? id_cor { get; set; }
        public string cor { get; set; }

        public int? id_grv { get; set; }
        public int? id_tarifa_tipo_veiculo { get; set; }
        public int? id_cliente { get; set; }
        public int? id_deposito { get; set; }
        public int? id_reboquista { get; set; }
        public int? id_reboque { get; set; }
        public int? id_autoridade_responsavel { get; set; }
        
        public int? id_detran_marca_modelo { get; set; }

        public Cliente cliente { get; set; }
        public Deposito deposito { get; set; }

        public string numero_formulario_grv { get; set; }        
        public string placa { get; set; }        
        public string chassi { get; set; }
        public string marca_modelo { get; set; }        
        public string tipo_veiculo { get; set; }
        
        public string flag_comboio { get; set; }
        public string reboque { get; set; }
        public string status { get; set; }
        public DateTime? data_hora_remocao { get; set; }
        public DateTime? data_hora_guarda { get; set; }
        public DateTime? data_cadastro { get; set; }

        public string lote { get; set; }

        public string municipio { get; set; }
        public string uf { get; set; }
    }
}
