using MobLink.WebLeilao.Dominio;
using MobLink.WebLeilao.Repositorio;
using MobLink.WebLeilao.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MobLink.WebLeilao.Web.Controllers
{
    [Authorize]
    [PermissoesFiltro(Roles = "CONFERENCIAPATIO")]
    public class PatioController : Controller
    {
        public ActionResult Index(FormCollection FORM, string Ordenacao=null, string Localizacao=null, string TipoVeiculo=null, 
                                  string Placa=null, string Chassi=null, string Processo=null, string Msg=null)
        {

            var id_tipo_veiculo = FORM["TIPO_VEICULO"];
            var id_localizacao = FORM["LOCALIZACAO"];

            IEnumerable<Lote> res;

            if (!string.IsNullOrEmpty(Msg))
            {
                ViewBag.Msg = Msg;
            }
            else
            {
                ViewBag.Msg = "";
            }

            if (!string.IsNullOrEmpty(Placa))
                res = RepositorioGlobal.Lote.SelecionarTudoPatio().ToList().Where(p => p.placa.Contains(Placa.Trim().ToUpper()));

            else if (!string.IsNullOrEmpty(Chassi))
                res = RepositorioGlobal.Lote.SelecionarTudoPatio().ToList().Where(p => p.chassi.Contains(Chassi.Trim().ToUpper()));

            else if (!string.IsNullOrEmpty(Processo))
                res = RepositorioGlobal.Lote.SelecionarTudoPatio().ToList().Where(p => p.numero_formulario_grv.Contains(Processo.Trim().ToUpper()));

            else if (!string.IsNullOrEmpty(Localizacao))
                res = RepositorioGlobal.Lote.SelecionarTudoPatio().ToList().Where(p => p.localizacao.Contains(Localizacao.Trim().ToUpper()));

            else if (!string.IsNullOrEmpty(TipoVeiculo))
                res = RepositorioGlobal.Lote.SelecionarTudoPatio().ToList().Where(p => p.tipo_veiculo.Contains(TipoVeiculo.Trim().ToUpper()));

            else
                //res = lr.SelecionarTudoPatio().ToList();
                res = new List<Lote>();

            if (!string.IsNullOrEmpty(Ordenacao))
            {
                switch (Ordenacao)
                {
                    case "TipoVeiculo":
                        res = res.OrderBy(p => p.tipo_veiculo);
                        break;
                    case "Localizacao":
                        res = res.OrderBy(p => p.localizacao);
                        break;
                    case "Lote":
                        res = res.OrderBy(p => p.numero_lote);
                        break;
                    case "Processo":
                        res = res.OrderBy(p => p.numero_formulario_grv);
                        break;
                    case "Placa":
                        res = res.OrderBy(p => p.placa);
                        break;
                    case "Chassi":
                        res = res.OrderBy(p => p.chassi);
                        break;
                    case "MarcaModelo":
                        res = res.OrderBy(p => p.marca_modelo);
                        break;
                    default:
                        break;
                }
            }

            return View(res);
        }

        public ActionResult Edit(int id)
        {
            return View(RepositorioGlobal.Lote.SelecionarPorId(id));
        }

        [HttpPost]
        public ActionResult Edit(Lote Lote, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                Lote.Conferido_Patio = string.IsNullOrEmpty(form["flag_concluido"]) ? "N" : "S";
                
                RepositorioGlobal.Lote.AlterarPatio(Lote);

                ViewBag.Msg = string.Format("Conferência realizada com sucesso para o lote {0}, do Leilão {1}", Lote.numero_lote, Lote.nome_leilao);
                return RedirectToAction("Index", new { Msg = ViewBag.Msg });
            }

            return View();
        }
    }
}