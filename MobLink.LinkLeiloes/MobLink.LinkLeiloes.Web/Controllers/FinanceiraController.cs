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
    [PermissoesFiltro(Roles = "FINANCEIRAS")]
    public class FinanceiraController : Controller
    {
        public ActionResult Index()
        {
            return View(RepositorioGlobal.Financeira.SelecionarTudo());
        }
        
        public ActionResult Details(int id)
        {
            return View();
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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
            return View(RepositorioGlobal.Financeira.SelecionarPorId(id));
        }
        
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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
        
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
