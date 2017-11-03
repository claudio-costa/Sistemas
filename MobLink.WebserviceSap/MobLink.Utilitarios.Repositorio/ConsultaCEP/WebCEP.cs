using MobLink.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.Utilitarios.Repositorio.ConsultaCEP
{
    public class WebCEP
    {
        public string cep { get; set; }

        public string tipo_logradouro { get; set; }

        public string logradouro { get; set; }

        public string bairro { get; set; }

        public string municipio { get; set; }

        public string estado { get; set; }

        public string ibge { get; set; }

        public WebCEP GetCepFromCorreios(WebCEP WebCEP)
        {
            Framework.WebServices.WebserviceCorreios.Endereco retorno;

            try
            {
                retorno = Framework.WebServices.WebserviceCorreios.ConsultaCEP(WebCEP.cep);
            }
            catch (Exception e)
            {
                return new WebCEP();
            }

            return new WebCEP()
            {
                bairro = retorno.bairro,
                cep = retorno.cep,
                estado = retorno.uf,
                ibge = "",
                logradouro = retorno.end,
                municipio = retorno.cidade,
                tipo_logradouro = retorno.end.SomentePrimeiraPalavra()
            };
        }
    }

    

}
