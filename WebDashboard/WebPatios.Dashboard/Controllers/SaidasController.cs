using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPatios.Dashboard.Controllers
{
    [Authorize]
    public class SaidasController : Controller
    {
        // GET: Saidas
        public ActionResult Index()
        {
            ViewBag.Title = "Saídas";

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

            var sai = new Business.SaidaBLL(filtro);

            ViewBag.Saidas = sai.getSaidas();
            ViewBag.SaidasDiaSemana = sai.getSaidasDiaSemana();
            ViewBag.SaidasTipoVeiculo = sai.getSaidasTipoVeiculo();

            return View();
        }
    }
}