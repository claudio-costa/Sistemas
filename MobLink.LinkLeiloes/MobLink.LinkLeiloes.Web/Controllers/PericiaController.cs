using MobLink.Framework;
using MobLink.LinkLeiloes.Dominio;
using MobLink.LinkLeiloes.Repositorio;
using MobLink.LinkLeiloes.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobLink.LinkLeiloes.Web.Controllers
{
    [Authorize]
    public class PericiaController : Controller
    {
        [PermissoesFiltro(Roles = "STATUSPERICIA")]
        public ActionResult Status()
        {
            return View(RepositorioGlobal.Pericia.ListaStatus());
        }

        [PermissoesFiltro(Roles = "STATUSVEICULOSPERICIA")] 
        public ActionResult StatusVeiculos()
        {
            return View(RepositorioGlobal.Pericia.ListaStatusVeiculos());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OS()
        {
            ViewBag.Leiloes = new SelectList(RepositorioGlobal.Leilao.SelecionarTudo(), "id", "Descricao", null);
            ViewBag.Status = new SelectList(RepositorioGlobal.StatusLote.SelecionarTudo(), "id", "Descricao", null);
            return View();
        }

        public PartialViewResult CarregarLotes(FormCollection model)
        {
            var id = model["IDLEILAO"];
            var idstatus = model["IDSTATUS"];

            if (!string.IsNullOrEmpty(id))
            {
                int idleilao = int.Parse(id);
                var LoteRepositorio = 
                    RepositorioGlobal.Lote.SelecionarTudo(idleilao).Where(p=>p.id_status_lote == idstatus.ToInt32()).ToList();

                return PartialView("_CarregarLotes", LoteRepositorio);
            }
            else
            {
                return PartialView("_CarregarLotes", new List<Lote>());
            }
        }

        [HttpPost] public ActionResult OS(int IDLEILAO, int IDSTATUS, FormCollection form)
        {
            ViewBag.Leiloes = new SelectList(RepositorioGlobal.Leilao.SelecionarTudo(), "id", "Descricao", null);
            ViewBag.Status = new SelectList(RepositorioGlobal.StatusLote.SelecionarTudo(), "id", "Descricao", null);
            return View();
        }

        public PartialViewResult EnviarOrdemServico(FormCollection form)
        {
            int j = 0;

            for (int i = 0; i < 100000000; i++)
            {
                j++;
            }

            ViewBag.Msg = "OPERAÇÃO REALIZADA COM SUCESSO!";
            return PartialView("_CarregarLotes", new List<Lote>());
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost] public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost] public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost] public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
