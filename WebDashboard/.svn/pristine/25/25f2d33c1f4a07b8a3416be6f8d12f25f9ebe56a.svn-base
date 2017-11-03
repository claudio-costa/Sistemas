using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPatios.Dashboard.Controllers
{
    [Authorize]
    public class EntradasController : Controller
    {
        // GET: Entradas
        public ActionResult Index()
        {
            ViewBag.Title = "Entradas";

            Model.Deposito filtro = (HttpContext.Application["FILTRO"] != null &&
                                     !string.IsNullOrEmpty(HttpContext.Application["FILTRO"].ToString())) ?
                                     (Model.Deposito)HttpContext.Application["FILTRO"] : new Model.Deposito()
                                     {
                                         descricaoDeposito = "NENHUM PÁTIO SELECIONADO",
                                         clienteDeposito = new Model.Cliente()
                                         {
                                             nomeCliente = "NENHUM"
                                         }
                                     };

            ViewBag.DepositoFiltrado = filtro;


            var ent = new Business.EntradaBLL(filtro);

            ViewBag.Entradas = ent.getEntradas("");
            ViewBag.EntradasTipoVeiculo = ent.getEntradasTipoVeiculo();            
            ViewBag.EntradasDiaSemana = ent.getEntradasDiaSemana();
            ViewBag.EntradasHora = ent.getEntradasHora();           

            return View();
        }
    }
}