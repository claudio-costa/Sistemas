using MobLink.WebLeilao.Repositorio;
using MobLink.WebLeilao.Web.Security;
using System.Web.Mvc;

namespace MobLink.WebLeilao.Web.Controllers
{
    [Authorize]
    [PermissoesFiltro(Roles = "COMITENTES")]
    public class ComitenteController : Controller
    {   
        public ActionResult Index()
        {
            return View(RepositorioGlobal.Comitente.SelecionarTudo());
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
            ViewBag.TiposImportacao = RepositorioGlobal.Comitente.SelecionarTiposImportacao();

            return View(RepositorioGlobal.Comitente.SelecionarPorId(id));
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
