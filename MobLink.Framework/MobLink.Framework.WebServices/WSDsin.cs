using System;
using System.Net;

namespace MobLink.Framework.WebServices
{
    public class WSDsin
    {
        private WebClient _WebClient = new WebClient();
        private Uri _Uri;

        ~WSDsin()
        {
            _WebClient.Dispose();
        }

        public WSDsin(Ambientes Ambiente)
        {
            try
            {
                var p = Setup.WebServices.WsDsin(Ambiente);  // Parametros.URL.Webservices.WSDsin(Ambiente);

                _Uri = new Uri(p.Url);

                CredentialCache credentialCache = new CredentialCache();

                credentialCache.Add(_Uri, "Basic", new NetworkCredential(p.Usuario, p.Senha));

                _WebClient.Credentials = credentialCache;

                _WebClient.Headers.Add("authorization", "Basic TGVpbGFvUkw6U0xlaWxhb1JM");
            }
            catch
            {
                throw new Exception("Erro ao inicializar o webservice WSDsin");
            }
        }

        public string ConsultaDadosLotes(string dataLeilao, int qtdeLotes, int qtdeDiasPatio, string depositos)
        {
            var parametros = new System.Collections.Specialized.NameValueCollection();

            parametros.Add("method", "consultaDadosLotes");

            parametros.Add("dataLeilao", string.Format("{0}", dataLeilao));
            parametros.Add("qtdeLotes", string.Format("{0}", qtdeLotes));
            parametros.Add("qtdeDiasPatio", string.Format("{0}", qtdeDiasPatio));
            parametros.Add("depositos", string.Format("{0}", depositos));

            _WebClient.QueryString = parametros;
            var jsonResponse = _WebClient.DownloadString(_Uri);

            return jsonResponse;

            //var jsonResponse = client.DownloadString("http://patiodemo.dsin.com.br/webService/webServiceSiol.php?method=consultaDadosLotesLeilao&idLeilao=DT05.16");
            //jsonResponse = client.DownloadString("http://patiodemo.dsin.com.br/webService/webServiceSiol.php?method=consultaDadosLotes&dataLeilao=2017-02-20&qtdeLotes=5&qtdeDiasPatio=30&depositos=21,1");
            //var RES = JsonConvert.DeserializeObject<Rootobject>(jsonResponse);
            //var z = JsonConvert.DeserializeObject<Rootobject>(jsonResponse);
        }

        public string ConsultaDadosLotesLeilao(string Leilao)
        {
            var parametros = new System.Collections.Specialized.NameValueCollection();

            parametros.Add("method", "consultaDadosLotesLeilao");
            parametros.Add("idLeilao", string.Format("{0}", Leilao));

            _WebClient.QueryString = parametros;
            var jsonResponse = _WebClient.DownloadString(_Uri);

            return jsonResponse;

            //var jsonResponse = client.DownloadString("http://patiodemo.dsin.com.br/webService/webServiceSiol.php?method=consultaDadosLotesLeilao&idLeilao=DT05.16");
            //jsonResponse = client.DownloadString("http://patiodemo.dsin.com.br/webService/webServiceSiol.php?method=consultaDadosLotes&dataLeilao=2017-02-20&qtdeLotes=5&qtdeDiasPatio=30&depositos=21,1");
            //var RES = JsonConvert.DeserializeObject<Rootobject>(jsonResponse);
            //var z = JsonConvert.DeserializeObject<Rootobject>(jsonResponse);
        }

        //public static string ConsultaDadosLotes(string dataLeilao, int qtdeLotes, int qtdeDiasPatio, string depositos)
        //{
        //    using (WebClient client = new WebClient())
        //    {
        //        var p = Parametros.URL.Webservices.WSDsin();

        //        Uri Uri = new Uri(p.URL);

        //        CredentialCache credentialCache = new CredentialCache();

        //        credentialCache.Add(Uri, "Basic", new NetworkCredential(p.Usuario, p.Senha));

        //        client.Credentials = credentialCache;

        //        client.Headers.Add("authorization", "Basic TGVpbGFvUkw6U0xlaWxhb1JM");

        //        var parametros = new System.Collections.Specialized.NameValueCollection();

        //        parametros.Add("method", "consultaDadosLotes");

        //        parametros.Add("dataLeilao", string.Format("{0}", dataLeilao));
        //        parametros.Add("qtdeLotes", string.Format("{0}", qtdeLotes));
        //        parametros.Add("qtdeDiasPatio", string.Format("{0}", qtdeDiasPatio));
        //        parametros.Add("depositos", string.Format("{0}", depositos));

        //        client.QueryString = parametros;
        //        var jsonResponse = client.DownloadString(Uri);

        //        return jsonResponse;

        //        //var jsonResponse = client.DownloadString("http://patiodemo.dsin.com.br/webService/webServiceSiol.php?method=consultaDadosLotesLeilao&idLeilao=DT05.16");
        //        //jsonResponse = client.DownloadString("http://patiodemo.dsin.com.br/webService/webServiceSiol.php?method=consultaDadosLotes&dataLeilao=2017-02-20&qtdeLotes=5&qtdeDiasPatio=30&depositos=21,1");
        //        //var RES = JsonConvert.DeserializeObject<Rootobject>(jsonResponse);
        //        //var z = JsonConvert.DeserializeObject<Rootobject>(jsonResponse);
        //    }
        //}

        //public static string ConsultaDadosLotesLeilao(string Leilao)
        //{
        //    using (WebClient client = new WebClient())
        //    {
        //        Uri Uri = new Uri("http://patio.dsin.com.br/webService/webServiceSiol.php");

        //        CredentialCache credentialCache = new CredentialCache();

        //        credentialCache.Add(Uri, "Basic", new NetworkCredential("LeilaoRL", "SLeilaoRL"));

        //        client.Credentials = credentialCache;

        //        client.Headers.Add("authorization", "Basic TGVpbGFvUkw6U0xlaWxhb1JM");

        //        var parametros = new System.Collections.Specialized.NameValueCollection();

        //        parametros.Add("method", "consultaDadosLotesLeilao");
        //        parametros.Add("idLeilao", string.Format("{0}", Leilao));

        //        client.QueryString = parametros;
        //        var jsonResponse = client.DownloadString(Uri);

        //        return jsonResponse;

        //        //var jsonResponse = client.DownloadString("http://patiodemo.dsin.com.br/webService/webServiceSiol.php?method=consultaDadosLotesLeilao&idLeilao=DT05.16");
        //        //jsonResponse = client.DownloadString("http://patiodemo.dsin.com.br/webService/webServiceSiol.php?method=consultaDadosLotes&dataLeilao=2017-02-20&qtdeLotes=5&qtdeDiasPatio=30&depositos=21,1");
        //        //var RES = JsonConvert.DeserializeObject<Rootobject>(jsonResponse);
        //        //var z = JsonConvert.DeserializeObject<Rootobject>(jsonResponse);
        //    }
        //}
    }
}
