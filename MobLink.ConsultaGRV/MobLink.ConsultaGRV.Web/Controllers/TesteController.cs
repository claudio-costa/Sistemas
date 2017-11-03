using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobLink.ConsultaGRV.Web.Controllers
{
    public class TesteController : Controller
    {
        // GET: Teste
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string placa, string chassi, string grv, FormCollection form, Model.GRV.GRV grvBoleto)
        {
            var arquivo = form["arq"];

            //Response.Clear();
            //MemoryStream ms = new MemoryStream(arquivo);
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=BOLETO.pdf");
            //Response.Buffer = true;
            //ms.WriteTo(Response.OutputStream);
            //Response.End();

            HomeController hc = new HomeController();

            return hc.Index(placa, chassi, grv, form, grvBoleto);
        }

        public void GeraPDF(byte[] arq, FormCollection form)
        {
            Response.Clear();
            MemoryStream ms = new MemoryStream(arq);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=BOLETO.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
        }


    }
}