using MobLink.LinkLeiloes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobLink.LinkLeiloes.Web
{
    public class Constantes
    {
        public const string NOME_APP = "LINK Leilões";

        public static Usuario UsuarioLogado { get; set; }
    }
}