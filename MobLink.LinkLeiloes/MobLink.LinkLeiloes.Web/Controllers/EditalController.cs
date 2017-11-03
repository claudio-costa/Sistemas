using MobLink.WebLeilao.Dominio;
using MobLink.WebLeilao.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MobLink.WebLeilao.Web.Controllers
{
    [Authorize]
    public class EditalController : Controller
    {
        // GET: Edital
        public ActionResult Index()
        {
            if (TempData["MSG"] != null)
            {
                ViewBag.Msg = TempData["MSG"];
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.Erro = "É NECESSÁRIO ESCOLHER UM LEILÃO";
                return View();
            }

            //if (Request.Form.Count > 0)
            //{
                
                //var editais = RepositorioGlobal.Edital.SelecionarTudo(new Dominio.Leilao() { Id = Convert.ToInt32(idLeilao) });

                return View(RepositorioGlobal.Leilao.SelecionarPorId(id.Value));
            //}
            
        }

        public ActionResult GerarEdital(int id)
        {
            RepositorioGlobal.Edital.Inserir(new Dominio.Edital() { id_leilao = id, id_usuario_geracao = Constantes.UsuarioLogado.Id });
            TempData["MSG"] = "EDITAL GERADO COM SUCESSO";
            return RedirectToAction("Index", new { id = id });
        }

        private FileResult Edital()
        {
            var cliente = new { ContactName = "CLAUDIO", City = "NITEROI", Country = "BRASIL", Phone = "9999-9999" };

            HttpContext.Response.Clear();
            HttpContext.Response.Charset = "";
            HttpContext.Response.ContentType = "application/msword";

            var strNomeArquivo = "DocumentoGerado1" + ".doc";

            HttpContext.Response.AddHeader("Content-Disposition", "inline;filename=" + strNomeArquivo);

            var strHTMLContent = new StringBuilder();
            strHTMLContent.Append("<h1 title='Heading' align='Center' style='font-family: verdana; font -size: 80 % ; color: black'><u> Documento - Word - tabela </u> </h1> ").ToString();
            strHTMLContent.Append("<br>".ToString());
            strHTMLContent.Append("<table align='Center'>".ToString());
            strHTMLContent.Append("<tr>".ToString());
            strHTMLContent.Append("<td style='width:100px; background:# 99CCE0'><b>Nome </b> </td>".ToString());
            strHTMLContent.Append("<td style='width:100px; background:# 99CCE0'><b>Cidade </b> </td>".ToString());
            strHTMLContent.Append("<td style='width:100px; background:# 99CCE0'><b>Pais </b> </td>".ToString());
            strHTMLContent.Append("<td style='width:100px; background:# 99CCE0'><b>Fone </b> </td>".ToString());
            strHTMLContent.Append("</tr>".ToString());

            //primeira linha de dados - a tabela
            strHTMLContent.Append("<tr>".ToString());
            strHTMLContent.Append("<td style='width:100px'>" + cliente.ContactName + " </td>".ToString());
            strHTMLContent.Append("<td style='width:100px'>" + cliente.City + " </td>".ToString());
            strHTMLContent.Append("<td style='width:100px'>" + cliente.Country + "</td>".ToString());
            strHTMLContent.Append("<td style='width:100px'>" + cliente.Phone + "</td>".ToString());
            strHTMLContent.Append("</tr>".ToString());
            strHTMLContent.Append("</table>".ToString());
            strHTMLContent.Append("<br><br>".ToString());

            //segunda linha de dados: o texto

            strHTMLContent.Append("<b> " + cliente.ContactName + "</b>" + " vem por meio desta declarar que ".ToString());
            strHTMLContent.Append(" reside em  <b>" + cliente.City + "</b> -  <b>" + cliente.Country + "</b>".ToString());
            strHTMLContent.Append(" telefone : <b>" + cliente.Phone + "</b>".ToString());
            strHTMLContent.Append("<br><br>".ToString());
            strHTMLContent.Append("</table>".ToString());
            strHTMLContent.Append("<br><br>".ToString());
            strHTMLContent.Append("<p align='Center'> Documento Word gerado dinamicamente </p> ".ToString());
            HttpContext.Response.Write(strHTMLContent);
            //HttpContext.Current.Response.[End]();
            HttpContext.Response.Flush();

            return File(strNomeArquivo, "application/msword");
        }
    }
}