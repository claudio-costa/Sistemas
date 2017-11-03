using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobLink.WebLeilao.Dominio
{
    public class ProprietarioSPE
    {
        [Required]
        public string NOME { get; set; }
        [Required]
        public string PLACA { get; set; }
        [Required]
        public string ENDERECO { get; set; }
        [Required]
        public string NUMERO { get; set; }
        [Required]
        public string COMPLEMENTO { get; set; }
        [Required]
        public string BAIRRO { get; set; }
        [Required]
        public string UF { get; set; }
        [Required]
        public string MUNICIPIO { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string FORMULARIO_GRV { get; set; }
        [Required]
        public string MARCA_MODELO { get; set; }
        [Required]
        public string RENAVAM { get; set; }
        [Required]
        public string DATA_HORA_APREENSAO { get; set; }
        [Required]
        public string NOME_DEPOSITO { get; set; }
        [Required]
        public string ENDERECO_DEPOSITO { get; set; }
    }
}