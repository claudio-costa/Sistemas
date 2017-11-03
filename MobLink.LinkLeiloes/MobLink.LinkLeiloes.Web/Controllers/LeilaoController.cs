using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MobLink.LinkLeiloes.Dominio;
using MobLink.LinkLeiloes.Repositorio;
using System;
using MobLink.LinkLeiloes.Web.Security;
using System.IO;
using System.Text;
using MobLink.Framework;

namespace MobLink.LinkLeiloes.Web.Controllers
{
    [Authorize]
    [PermissoesFiltro(Roles = "LEILOES")]
    public class LeilaoController : Controller
    {
        public ActionResult Despesas(int id)
        {
            ViewBag.idleilao = id;
            return View(RepositorioGlobal.Despesa.SelecionarDespesasLeilao(id));
        }

        public ActionResult ExcluirDespesa(int id, int idleilao)
        {
            RepositorioGlobal.Despesa.ExcluirDespesaLeilao(id);
            return RedirectToAction("Despesas", new { id = idleilao });
        }

        public ActionResult InserirDespesa(FormCollection form)
        {
            var leilao = form["leilao"];
            var despesa = form["despesa"];
            var valor = form["valor"];

            //Grava a despesa
            var ent = new Despesa_Leilao()
            {
                Id_Despesa = Convert.ToInt32(despesa),
                Id_Leilao = Convert.ToInt32(leilao),
                Valor = Convert.ToDouble(valor)
            };

            RepositorioGlobal.Despesa.InserirDespesaLeilao(ent);

            return RedirectToAction("Despesas", new { id = leilao });
        }

        public ActionResult Index()
        {
            return View(RepositorioGlobal.Leilao.SelecionarTudo(RepositorioGlobal.Status.Ativo).ToList());
        }

        public void Excel(int id)
        {
            var sql = string.Format(@"
            SELECT numero_lote NUMERO_LOTE, numero_formulario_grv PROCESSO, placa PLACA, chassi CHASSI, marca_modelo MARCA_MODELO, 
                   valor_avaliacao AVALIACAO, lance_minimo LANCE_MINIMO,
		           (SELECT descricao FROM tb_lotes_status WHERE id_status_lote = tb_lotes_status.id) STATUS,
		           cor COR, ANO_FABRICACAO, ANO_MODELO, tipo_combustivel COMBUSTIVEL, numero_motor NUMERO_MOTOR, 
                   CONVERT(VARCHAR(MAX), OBSERVACOES) OBSERVACOES
              FROM tb_leilao_lotes 
             WHERE id_leilao = {0}
             ORDER BY numero_lote", id);

            var model = RepositorioGlobal.Util.ConsultaGenerica(Framework.Util.DetectarConexao(), sql);

            //var model = RepositorioGlobal.Lote.SelecionarTudo(id).ToList();

            Export export = new Export();
            export.ToExcel_XLSX(Response, model, "Listagem_Lotes");
        }

        public ActionResult PrestacaoContas()
        {
            return View(RepositorioGlobal.Leilao.SelecionarTudo(RepositorioGlobal.Status.Pagamento).ToList());
        }

        public ActionResult NotificacaoProprietario(int id, FormCollection form)
        {
            var GERAR = form["GERAR"];

            if (GERAR != null)
            {
                GerarNotificacaoSPE(id);
            }

            return View(Repositorio.RepositorioGlobal.Leilao.SelecionarPorId(id));
        }

        public ActionResult LeiloesFinalizados()
        {
            //return View(RepositorioGlobal.Leilao.SelecionarTudo(RepositorioGlobal.Status.Finalizado).ToList());
            return View();
        }

        public ActionResult LeiloesRealizados()
        {
            return View(RepositorioGlobal.Leilao.SelecionarTudo(RepositorioGlobal.Status.Pagamento).ToList());
        }

        private List<Arquivo> ListaArquivos(string caminho)
        {
            List<Arquivo> lstArquivos = new List<Arquivo>();
            //DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath(caminho));
            DirectoryInfo dirInfo = new DirectoryInfo(caminho);

            int i = 0;
            foreach (var item in dirInfo.GetFiles())
            {
                lstArquivos.Add(new Arquivo()
                {
                    IDArquivo = i + 1,
                    Nome = item.Name,
                    Caminho = dirInfo.FullName + @"\" + item.Name
                });
                i = i + 1;
            }
            return lstArquivos;
        }

        public FileResult Download(string id)
        {
            int _arquivoId = Convert.ToInt32(id);
            string contentType = "";
            var arquivos = ListaArquivos(Server.MapPath("~/Content/Arquivos/PosLeilao/PrestacaoContas"));
            string nomeArquivo = (from arquivo in arquivos
                                  where arquivo.IDArquivo == _arquivoId
                                  select arquivo.Caminho).First();
            string extensao = Path.GetExtension(nomeArquivo);
            string nomeArquivoV = Path.GetFileNameWithoutExtension(nomeArquivo);
            if (extensao.Equals(".pdf"))
                contentType = "application/pdf";
            if (extensao.Equals(".JPG") || extensao.Equals(".GIF") || extensao.Equals(".PNG"))
                contentType = "application/image";
            if (extensao.Equals(".xlsx"))
                contentType = "application/excel";

            return File(nomeArquivo, contentType, nomeArquivoV + extensao);
        }

        [HttpPost]
        public ActionResult AlterarStatus(int id, FormCollection form)
        {
            var status = form["id_status"];

            RepositorioGlobal.Leilao.AlterarStatus(id, int.Parse(status));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(int arrematante)
        {
            return View(RepositorioGlobal.Leilao.SelecionarTudo().ToList());
        }

        public ActionResult Details(int id)
        {
            return View(RepositorioGlobal.Leilao.SelecionarPorId(id));
        }

        public ActionResult Create()
        {
            var Leiloeiros = RepositorioGlobal.Leiloeiro.SelecionarTudo();
            SelectList ListaLeiloeiros = new SelectList(Leiloeiros, "Id", "Nome");
            ViewBag.Leiloeiros = ListaLeiloeiros;

            var Comitentes = RepositorioGlobal.Comitente.SelecionarTudo();
            SelectList ListaComitentes = new SelectList(Comitentes, "Id", "Descricao");
            ViewBag.Comitentes = ListaComitentes;

            var Expositores = RepositorioGlobal.Expositor.SelecionarTudo();
            SelectList ListaExpositores = new SelectList(Expositores, "Id", "Descricao");
            ViewBag.Expositores = ListaExpositores;

            var Empresas = RepositorioGlobal.Empresa.SelecionarTudo();
            SelectList ListaEmpresas = new SelectList(Empresas, "Id", "Descricao");
            ViewBag.Empresas = ListaEmpresas;

            var Regras = RepositorioGlobal.RegraPrestacaoConta.SelecionarTudo();
            SelectList ListaRegras = new SelectList(Regras, "Id", "Descricao");
            ViewBag.RegrasPC = ListaRegras;

            return View(new Leilao());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Leilao collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var modelo = collection;
                    var NovoID = RepositorioGlobal.Leilao.InserirComRetorno(modelo);

                    if (NovoID == -1)
                    {
                        ViewBag.Erro = "JÁ EXISTE UM LEILÃO COM ESTA DESCRIÇÃO, ORDEM MATRIZ OU ORDEM LEILÃO.";
                    }
                    else
                    {
                        //return RedirectToAction("IngressarLotes", "Lote", new { Id = NovoID });
                        return RedirectToAction("Index", "Leilao");
                    }

                }

                var Leiloeiros = RepositorioGlobal.Leiloeiro.SelecionarTudo();
                SelectList ListaLeiloeiros = new SelectList(Leiloeiros, "Id", "Nome");
                ViewBag.Leiloeiros = ListaLeiloeiros;

                var Comitentes = RepositorioGlobal.Comitente.SelecionarTudo();
                SelectList ListaComitentes = new SelectList(Comitentes, "Id", "Descricao");
                ViewBag.Comitentes = ListaComitentes;

                var Expositores = RepositorioGlobal.Expositor.SelecionarTudo();
                SelectList ListaExpositores = new SelectList(Expositores, "Id", "Descricao");
                ViewBag.Expositores = ListaExpositores;

                var Empresas = RepositorioGlobal.Empresa.SelecionarTudo();
                SelectList ListaEmpresas = new SelectList(Empresas, "Id", "Descricao");
                ViewBag.Empresas = ListaEmpresas;

                var Regras = RepositorioGlobal.RegraPrestacaoConta.SelecionarTudo();
                SelectList ListaRegras = new SelectList(Regras, "Id", "Descricao");
                ViewBag.RegrasPC = ListaRegras;

                return View(collection);
            }
            catch (Exception e)
            {
                //var Leiloeiros = LeiloeiroRP.SelecionarTudo();
                //SelectList ListaLeiloeiros = new SelectList(Leiloeiros, "Id", "Nome");
                //ViewBag.Leiloeiros = ListaLeiloeiros;

                //var Comitentes = ComitenteRP.SelecionarTudo();
                //SelectList ListaComitentes = new SelectList(Comitentes, "Id", "Descricao");
                //ViewBag.Comitentes = ListaComitentes;

                //var Expositores = ExpositorRP.SelecionarTudo();
                //SelectList ListaExpositores = new SelectList(Expositores, "Id", "Descricao");
                //ViewBag.Expositores = ListaExpositores;

                //return View();
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            var Leiloeiros = RepositorioGlobal.Leiloeiro.SelecionarTudo();
            SelectList ListaLeiloeiros = new SelectList(Leiloeiros, "Id", "Nome");
            ViewBag.Leiloeiros = ListaLeiloeiros;

            var Comitentes = RepositorioGlobal.Comitente.SelecionarTudo();
            SelectList ListaComitentes = new SelectList(Comitentes, "Id", "Descricao");
            ViewBag.Comitentes = ListaComitentes;

            var Expositores = RepositorioGlobal.Expositor.SelecionarTudo();
            SelectList ListaExpositores = new SelectList(Expositores, "Id", "Descricao");
            ViewBag.Expositores = ListaExpositores;

            var StatusLeilao = RepositorioGlobal.Leilao.StatusDeLeilao();
            SelectList ListaStatusLeilao = new SelectList(StatusLeilao, "Id", "Descricao");
            ViewBag.StatusLeilao = ListaStatusLeilao;

            var leilao = RepositorioGlobal.Leilao.SelecionarPorId(id);

            return View(leilao);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Leilao Leilao)
        {
            try
            {
                RepositorioGlobal.Leilao.Alterar(Leilao);

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

        public JsonResult ConsultaCEP(string CEP)
        {
            var MeuCEP = RepositorioGlobal.Util.ConsultaCEP(CEP);

            return Json(new { MeuCEP = MeuCEP }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DETRO_005(string Msg)
        {
            if (!string.IsNullOrEmpty(Msg))
                ViewBag.Msg = Msg;

            var leiloes = RepositorioGlobal.Leilao.SelecionarTudoDETRO();

            ViewBag.Leiloes = new SelectList(leiloes, "id", "Descricao", null);

            return View();
        }

        public ActionResult DETRO_002(string Msg)
        {
            if (!string.IsNullOrEmpty(Msg))
                ViewBag.Msg = Msg;

            var leiloes = RepositorioGlobal.Leilao.SelecionarTudo();

            ViewBag.Leiloes = new SelectList(leiloes, "id", "Descricao", null);

            return View();
        }

        public PartialViewResult CarregarLotesDETRO(FormCollection model)
        {
            var id = model["IDLEILAO"];

            if (!string.IsNullOrEmpty(id))
            {
                int idleilao = int.Parse(id);
                var LoteRepositorio = RepositorioGlobal.Lote.SelecionarTudo(idleilao);
                return PartialView("_005", LoteRepositorio);
            }
            else
            {
                return PartialView("_005", new List<Lote>());
            }
        }

        public PartialViewResult CarregarLotes002(FormCollection model)
        {
            var id = model["IDLEILAO"];

            if (!string.IsNullOrEmpty(id))
            {
                int idleilao = int.Parse(id);
                var LoteRepositorio = RepositorioGlobal.Lote.SelecionarTudo(idleilao);
                return PartialView("_002", LoteRepositorio);
            }
            else
            {
                return PartialView("_002", new List<Lote>());
            }
        }



        public ActionResult GerarNotificacaoSPE(int IdLeilao)
        {
            StringBuilder sb = new StringBuilder();

            var proprietarios = MobLink.LinkLeiloes.Repositorio.RepositorioGlobal.Proprietario.SelecionarNotificacoes(IdLeilao);

            


            foreach (var r in proprietarios)
            {
                var lote = RepositorioGlobal.Lote.SelecionarPorId(r.id_lote);

                //var grv = RepositorioGlobal.GRV.SelecionarPorId(lote.id_grv.Value);

                //var sql_deposito = 
                //    string.Format("SELECT cliente_nome nome, cliente_endereco_completo endereco FROM vw_dep_clientes_depositos WHERE id_cliente = {0} AND id_cliente_deposito = {1}", grv.id_cliente, grv.id_deposito);

                //var deposito = RepositorioGlobal.Util.ConsultaGenerica(sql_deposito, Util.LerConfiguracao("CONEXAO_DP")).ConverterParaEntidade<Cliente>();

                //PROPRIETARIO
                sb.Append("#");
                sb.Append("#" + r.nome_proprietario + "/" + r.placa);
                sb.Append("#" + r.endereco_proprietario + " " + r.numero_endereco_proprietario + " " + r.complemento_endereco_proprietario);
                sb.Append("#" + r.bairro_proprietario + "##-" + r.uf_proprietario);
                sb.Append("#" + r.descricao_municipio_endereco);
                sb.Append("#" + r.uf_proprietario);
                sb.Append("#" + r.cep_endereco_proprietario + "#BRASIL#########");
                sb.Append("#" + lote.numero_formulario_grv);     //CONFIRMAR NO SERVIÇO DE PRE LEILAO ESTE CAMPO
                sb.Append("#" + r.descricao_marca);
                sb.Append("#" + r.placa);
                sb.Append("#" + r.renavam);
                sb.Append("#" + lote.data_hora_apreensao);
                sb.Append("#" + "DEPOSITOTESTE");   //DEPOSITO
                sb.Append("#" + "ENDERECODEPOSITOTESTE");   //ENDEREÇO DO DEPOSITO
                sb.Append("#");
                sb.AppendLine("");
            }


            UTF8Encoding encoding = new UTF8Encoding();
            byte[] contentAsBytes = encoding.GetBytes(sb.ToString());

            HttpContext.Response.ContentType = "application/force-download";
            HttpContext.Response.ContentType = "application / octet - stream;";
            HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + "SPE-" + DateTime.Now.ToShortDateString() + ".txt");
            HttpContext.Response.Buffer = true;
            HttpContext.Response.Clear();
            HttpContext.Response.OutputStream.Write(contentAsBytes, 0, contentAsBytes.Length);
            HttpContext.Response.OutputStream.Flush();
            HttpContext.Response.End();

            return View();
        }
    }

}
