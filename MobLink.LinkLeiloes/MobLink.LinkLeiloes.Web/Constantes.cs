using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobLink.WebLeilao.Web
{
    public class Constantes
    {
        public const string NOME_APP = "LINK Leilões";

        public static Usuario UsuarioLogado { get; set; }
    }
}