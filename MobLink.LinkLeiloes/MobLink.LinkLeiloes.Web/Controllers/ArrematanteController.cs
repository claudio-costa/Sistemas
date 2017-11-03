using MobLink.LinkLeiloes.Dominio;
using MobLink.LinkLeiloes.Repositorio;
using MobLink.LinkLeiloes.Web.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MobLink.LinkLeiloes.Web.Controllers
{
    [Authorize]
    [PermissoesFiltro(Roles = "ARREMATANTES")]
    public class ArrematanteController : Controller
    { 
        public ActionResult Index(int id)
        {
            var arrematantes = RepositorioGlobal.Arrematante.SelecionarTudo(id).ToList();
            
            return View(arrematantes);
        }
        
        [HttpPost]
        public ActionResult Index(int idleilao, FormCollection form)
        {
            var leilao = RepositorioGlobal.Leilao.SelecionarPorId(idleilao);

            var arrematantes = ConsultaArrematantes(leilao.descricao);

            if (arrematantes.Count > 0)
            {
                try
                {
                    foreach (var item in arrematantes)
                    {
                        //INSERT DE ARREMATANTES
                        RepositorioGlobal.Arrematante.Inserir(item);

                        //ALTERAR STATUS DOS LOTES
                        var lote = RepositorioGlobal.Lote.SelecionarPorProcesso(item.numero_processo);
                        lote.id_status_lote = 22;    //LOTE ARREMATADO
                        RepositorioGlobal.Lote.Alterar(lote);
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return View(arrematantes);
        }

        private List<Arrematante> ConsultaArrematantes(string DescricaoLeilao)
        {
            //MobLink.Framework.WebServices.WSVipBoleto wSVipBoleto = new Framework.WebServices.WSVipBoleto(Framework.Ambientes.Desenvolvimento);
            //var res = wSVipBoleto.FinanceiroDetalhamento(DescricaoLeilao);


            //if (res == "Usuario ou senha incorretos")
            //{
            //    ViewBag.Erro = "USUARIO OU SENHA INCORRETOS";
            //    return new List<Arrematante>();
            //}

            //if (res == "[]")
            //{
            //    ViewBag.Erro = "NENHUM RETORNO OBTIDO";
            //    return new List<Arrematante>();
            //}

            //return JsonConvert.DeserializeObject<List<Arrematante>>(res);

            return new List<Arrematante>();
        }
    }
}