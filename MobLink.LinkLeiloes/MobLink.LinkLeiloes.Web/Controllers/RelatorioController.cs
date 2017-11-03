using System.Web.Mvc;
using MobLink.WebLeilao.Web.Security;
using System.Collections.Generic;
using MobLink.WebLeilao.Repositorio;
using MobLink.Framework;
using System.Data;

namespace MobLink.WebLeilao.Web.Controllers
{
    [Authorize]
    //[PermissoesFiltro(Roles = "RELATORIOS")]
    public class RelatorioController : Controller
    {
        [PermissoesFiltro(Roles = "RELPRESTACAOCONTASLEILAO")] 
        public ActionResult PrestacaoContasLeilao()
        {
            return View();
        }

        [PermissoesFiltro(Roles = "RELPRESTACAOCONTASLOTE")] 
        public ActionResult PrestacaoContasLote()
        {
            return View();
        }

        [PermissoesFiltro(Roles = "RELRETORNOFINANCEIRO")]
        public ActionResult PrevisaoRetornoFinanceiro()
        {
            return View();
        }

        public ActionResult Recebimentos_Arrematantes(int id, FormCollection form)  //id_leilao
        {
            var opt = form["OPCAO"];
            var excel = form["excel"];

            ViewBag.Title = "Recebimentos " + Repositorio.RepositorioGlobal.Leilao.SelecionarPorId(id).descricao;

            DataTable lista = new DataTable();

            if (opt != null)
            {
                lista = Repositorio.RepositorioGlobal.Boletos.GetList_arrematante_recebimento(id, opt);
            }
            else
            {
                lista = Repositorio.RepositorioGlobal.Boletos.GetList_arrematante_recebimento(id);
            }

            if (excel != null)
            {
                Excel(lista);
            }

            return View(lista.ConverterParaLista<Dominio.ArrematantePagamento>());
        }

        public void Excel(DataTable lista)
        {   
            Export export = new Export();
            export.ToExcel_XLSX(Response, lista, "Relatório_de_Recebimentos");
        }

    }
}