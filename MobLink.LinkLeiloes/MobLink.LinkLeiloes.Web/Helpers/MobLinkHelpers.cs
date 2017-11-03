using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobLink.LinkLeiloes.Web
{
    public static class MobLinkHelpers
    {
        //public static MvcHtmlString Label_Macoratti(this HtmlHelper helper, string destino, string texto)
        //{
        //    return MvcHtmlString.Create(string.Format("<label for='{0}'>{1}</label>", destino, texto));
        //}

        public static MvcHtmlString MobLink_LinkJanelaModal(this HtmlHelper helper, string DescricaoLink, string NomeTela)
        {
            return MvcHtmlString.Create(string.Format("<a href='' data-toggle='modal' data-target='#{0}'>{1}</a>", NomeTela, DescricaoLink));
        }
    }
}