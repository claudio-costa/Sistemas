using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.Dominio
{

    public class LoteDSIN
    {
        public string COD_RETORNO { get; set; }
        public REGISTRO[] REGISTROS { get; set; }
    }

    public class REGISTRO
    {
        public int PATIO { get; set; }
        public string PROCESSO { get; set; }
        public string PLACA { get; set; }
        public string CHASSI { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string COR { get; set; }
        public string RENAVAM { get; set; }
        public string NROMOTOR { get; set; }
        public string CHAVE { get; set; }
        public string RESTRICAO { get; set; }
        public string TIPOVEICULODESCRICAO { get; set; }
        public string MUNVEICULO { get; set; }
        public string UFVEICULO { get; set; }
        public string OBSERVACAO { get; set; }
        public string RESPREMOCAO { get; set; }
        public string VEICREBOCADO { get; set; }
        public string DATAAPREENSAO { get; set; }
        public string HORAAPREENSAO { get; set; }
        public string DATAENTRADA { get; set; }
        public string HORAENTRADA { get; set; }
        public string LOCALIZACAO { get; set; }
        public string DEPOSITO { get; set; }
        public string COMBUSTIVEL { get; set; }
        public string ANOFABRICACAO { get; set; }
        public string ANOMODELO { get; set; }
        public object ESTADOVEICULO { get; set; }
        public string NOMEPROPRIETARIO { get; set; }
        public string ENDERECOPROPRIETARIO { get; set; }
        public string LOTE { get; set; }
        public int LANCEMINIMO { get; set; }
    }
}

