namespace MobLink.Framework.WebServices
{
    public class WebserviceCorreios
    {
        public class Endereco
        {
            public long Id { get; set; }
            public string bairro { get; set; }
            public string cep { get; set; }
            public string cidade { get; set; }
            public string complemento { get; set; }
            public string complemento2 { get; set; }
            public string end { get; set; }
            public string uf { get; set; }
        }

        public static Endereco ConsultaCEP(string cep)
        {
            Correios.AtendeClienteService acs = new Correios.AtendeClienteService();

            var res = acs.consultaCEP(cep);

            if (res != null)
            {
                return new Endereco()
                {
                    bairro = res.bairro.ToUpper(),
                    cep = res.cep.ToUpper(),
                    cidade = res.cidade.ToUpper(),
                    complemento = res.complemento.ToUpper(),
                    complemento2 = res.complemento2.ToUpper(),
                    end = res.end.ToUpper(),
                    Id = res.id,
                    uf = res.uf.ToUpper()
                };
            }
            else
            {
                return new Endereco();
            }


        }
    }
}
