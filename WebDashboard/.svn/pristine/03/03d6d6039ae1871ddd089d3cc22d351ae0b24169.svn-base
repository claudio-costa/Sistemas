using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPatios.Dashboard.Controllers
{
    [Authorize]
    public class EstoqueController : Controller
    {
        // GET: Estoque
        public ActionResult Index()
        {
            ViewBag.Title = "Estoque";

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

            var est = new Business.EstoqueBLL(filtro);

            ViewBag.EstoqueTipoVeiculo = est.getEstoqueTipoVeiculo();
            
            return View();
        }
    }
}