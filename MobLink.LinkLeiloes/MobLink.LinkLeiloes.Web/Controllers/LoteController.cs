using MobLink.LinkLeiloes.Dominio;
using MobLink.LinkLeiloes.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ExcelDataReader;
using System.IO;
using System.Data;
using Newtonsoft.Json;


namespace MobLink.LinkLeiloes.Web.Controllers
{
    [Authorize]
    public class LoteController : Controller
    {
        public ActionResult TransacoesExternas()
        {
            if (Request.Form.Count > 0)
            {
                var lotes = RepositorioGlobal.Lote.SelecionarTudo(int.Parse(Request.Form[0]));
                return View(lotes);
            }

            ViewBag.Leiloes = new SelectList(RepositorioGlobal.Leilao.SelecionarTudo().ToList(), "id", "Descricao", null);
            return View();
        }

        [HttpPost]
        public ActionResult ImportarPlanilha(HttpPostedFileBase upload, FormCollection form, Leilao leilao)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                Stream stream = upload.InputStream;

                IExcelDataReader reader = null;
                
                if (upload.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (upload.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {
                    //ModelState.AddModelError("File", "This file format is not supported");
                    TempData["msg"] = "Arquivo não suportado.";
                    return RedirectToAction("Index");
                }

                DataTable tabelaInicial = new DataTable();//= reader.AsDataSet().Tables[0];

                reader.Close();

                //Excluo as duas primeiras linhas
                tabelaInicial.Rows[0].Delete();
                //tabelaInicial.Rows[1].Delete();
                tabelaInicial.AcceptChanges();

                //Pego os títulos das colunas
                var titulos = tabelaInicial.Rows[0].ItemArray;

                //Apago a linha de títulos
                tabelaInicial.Rows[0].Delete();
                tabelaInicial.AcceptChanges();

                //Crio a table final
                DataTable tabelaFinal = new DataTable();

                //Adiciono as colunas da table final
                foreach (var item in titulos)
                {
                    tabelaFinal.Columns.Add(item.ToString());
                }

                //Adiciono os registros a table final
                foreach (DataRow item in tabelaInicial.Rows)
                {
                    if (item[0].ToString().Trim() != string.Empty)
                        tabelaFinal.Rows.Add(item.ItemArray);

                    //if (item[0].ToString().Trim() != string.Empty && item[1].ToString().Trim() != string.Empty)
                    //{
                    //    tabelaFinal.Rows.Add(item.ItemArray);
                    //}
                    //else
                    //{

                    //}
                }

                //Removo as colunas que não interessam na importação
                tabelaFinal.Columns.Remove("N");
                tabelaFinal.Columns.Remove("Status");
                tabelaFinal.Columns.Remove("Leilão");
                tabelaFinal.Columns.Remove("Lote");
                tabelaFinal.AcceptChanges();

                foreach (DataRow item in tabelaFinal.Rows)
                {
                    var valor = item[0];
                    valor = valor.ToString().Split('-').FirstOrDefault();
                    item[0] = valor;
                }

                tabelaFinal.AcceptChanges();

                //Adiciono a coluna Id do Leilao
                tabelaFinal.Columns.Add("IdLeilao");
                tabelaFinal.Columns.Add("Arquivo");

                foreach (DataRow item in tabelaFinal.Rows)
                {
                    item["IdLeilao"] = leilao.id;
                    item["Arquivo"] = upload.FileName;
                }

                foreach (DataRow item in tabelaFinal.Rows)
                {
                    if (item["Processo"].ToString().Trim() == "")
                    {
                        item.Delete();

                    }


                }

                tabelaFinal.AcceptChanges();
                TempData["arq"] = tabelaFinal;
                return RedirectToAction("SelecionarLotes", "Lote", leilao);
            }
            else
            {
                //ModelState.AddModelError("File", "Please Upload Your file");
                TempData["msg"] = "Por favor, aponte um arquivo e, em seguida, clique em upload.";
            }

            TempData["msg"] = "Houve algum erro na leitura do arquivo.";
            return RedirectToAction("Index", new { idLeilao = leilao.id });
        }

        [HttpPost]
        public ActionResult SelecionarLotes(FormCollection form)
        {
            //TODO: Tratar nenhuma GRV marcada para importação

            DataTable tabelaOriginal = new DataTable();
            var marcados = form.GetValues("marcados");

            DataTable query = ((DataTable)TempData["arq"])
                                .AsEnumerable()
                                .Where(r => marcados.Contains(r.Field<string>("Processo")))
                                .CopyToDataTable();

            var IdLeilao = query.Rows[0]["IdLeilao"].ToString();

            foreach (DataRow item in query.Rows)
            {
                var placa = item[1].ToString();

                //try
                //{
                //    PWSPatioxDetran ws = new PWSPatioxDetran();
                //    var retorno = ws.ConsultaVeiculo(placa, "ROOT");
                //    JavaScriptSerializer js = new JavaScriptSerializer();
                //var c = js.Deserialize<PWSPatioxDetran.RetornoConsultaVeiculo>(retorno);

                //    GRV lGRV = new GRV()
                //    {
                //        Chassi = c.Chassi,
                //        Cor = c.DescricaoCor,
                //        Placa = c.Placa,
                //        Marca_Modelo = c.DescricaoMarca,
                //        Tipo_Veiculo = c.DescricaoTipo,
                //        Id_Deposito = int.Parse(item[13].ToString().Substring(0, 3)),
                //        Numero_Formulario_GRV = item[0].ToString()
                //    };

                //    GRVRepositorio GRVRep = new GRVRepositorio();
                //    GRVRep.Inserir(lGRV, int.Parse(IdLeilao));
                //}
                //catch
                //{
                GRV lGRV = new GRV()
                {
                    chassi = item[2].ToString(),
                    cor = item[4].ToString(),
                    placa = item[1].ToString(),
                    marca_modelo = item[3].ToString(),
                    tipo_veiculo = item[5].ToString(),
                    //Id_Deposito = int.Parse(item[13].ToString().Substring(0, 3)),
                    numero_formulario_grv = item[0].ToString()

                    //Chassi = item[6].ToString(),
                    //Cor = item[8].ToString(),
                    //Placa = item[5].ToString(),
                    //Marca_Modelo = item[7].ToString(),
                    //Tipo_Veiculo = item[9].ToString(),
                    ////Id_Deposito = int.Parse(item[17].ToString().Substring(0, 3)),
                    //Numero_Formulario_GRV = item[1].ToString(),
                    //Status = item[2].ToString(),
                    //Lote = item[4].ToString(),

                };
                
                RepositorioGlobal.GRV.Inserir(lGRV, int.Parse(IdLeilao));
            }

            return RedirectToAction("Index", "Lote", new { id = IdLeilao });
        }

        [HttpPost]
        public ActionResult ImportacaoDSIN(int Id, FormCollection form, List<REGISTRO> CHECK)
        {
            var DEPOSITOS = form["DEPOSITOS"];
            var QTDE_DIAS = Convert.ToInt32(form["QTDE_DIAS"]);
            var QTDE_LOTES = Convert.ToInt32(form["QTDE_LOTES"]);

            var LEILAO = RepositorioGlobal.Leilao.SelecionarPorId(Id);

            ViewBag.QTDE_DIAS = QTDE_DIAS;
            ViewBag.QTDE_LOTES = QTDE_LOTES;
            ViewBag.Leilao = LEILAO;

            //GRAVAÇÃO
            try
            {
                var CHECSK = form["CHECK"].Split(',');

                if (CHECSK.Count() > 0)
                {
                    var vLOTES = RepositorioGlobal.Lote.ConsultaDadosLotes(LEILAO.data_leilao, QTDE_LOTES, QTDE_DIAS, DEPOSITOS);
                    var mJSON = JsonConvert.DeserializeObject<LoteDSIN>(vLOTES);

                    RepositorioGlobal.Lote.InserirLoteDSIN(mJSON, Id);
                    return RedirectToAction("Index", "Leilao");
                }
            }
            catch (Exception e)
            {

            }

            var LOTES = RepositorioGlobal.Lote.ConsultaDadosLotes(LEILAO.data_leilao, QTDE_LOTES, QTDE_DIAS, DEPOSITOS);

            try
            {
                var JSON = JsonConvert.DeserializeObject<Dominio.LoteDSIN>(LOTES);
                return View(JSON.REGISTROS.ToList());
            }
            catch
            {
                return View(new List<REGISTRO>());
            }
        }

        [HttpPost]
        public ActionResult AssociacaoDSIN(int Id, FormCollection form)
        {
            var NOME_LEILAO = form["NOME_LEILAO"];
            var LOTES = RepositorioGlobal.Lote.ConsultaDadosLotesLeilao(NOME_LEILAO);
            var JSON = JsonConvert.DeserializeObject<Dominio.LoteDSIN>(LOTES);

            if (JSON.REGISTROS != null)
            {
                RepositorioGlobal.Lote.InserirLoteDSIN(JSON, Id);
                return RedirectToAction("Index", "Leilao");
            }
            else
            {
                ViewBag.Erro = "NÃO FORAM ENCONTRADOS LOTES PARA ESTE LEILÃO";
                var LEILAO = RepositorioGlobal.Leilao.SelecionarPorId(Id);
                ViewBag.Leilao = LEILAO;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ImportarPatios(FormCollection form, Lote modelo)
        {
            var grv_marcado = form["marcados"];

            if (!string.IsNullOrEmpty(grv_marcado))
            {
                //Gravo a partir da grv
                var grvs = RepositorioGlobal.GRV.SelecionarTudo(grvs: grv_marcado.Replace("'", ""));
                
                foreach (var item in grvs)
                {


                    RepositorioGlobal.GRV.Inserir(item, modelo.id);

                    RepositorioGlobal.GRV.AtualizarStatusOperacao(item.id_grv.Value, "1");
                }

                //Redireciono o usuário para a lista de leilões
                ViewBag.Msg = "Lotes Ingressados com Sucesso!";
                return RedirectToAction("Index", "Leilao");
            }
            else
            {
                ViewBag.Erro = "Nenhum lote selecionado.";
            }
            
            ViewBag.clientes = new SelectList(RepositorioGlobal.Cliente.SelecionarTudo(), "Id_Cliente", "Nome");
            ViewBag.depositos = new SelectList(new List<Deposito>(), "Id_Deposito", "Descricao");

            return View(RepositorioGlobal.Leilao.SelecionarPorId(modelo.id));
        }

        [HttpPost]
        public JsonResult CarregaArquivo(int? id)
        {
            var r = new List<ArquivoPericia>();
            
            try
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

#if DEBUG
                    string savedFileName = Path.Combine(Server.MapPath("~/App_Data"), Path.GetFileName(hpf.FileName));
#else
                    string savedFileName = Path.Combine(Server.MapPath("~/Arquivos"), Path.GetFileName(hpf.FileName));
#endif

                    hpf.SaveAs(savedFileName);

                    ArquivoPericia ArquivoPericia = new ArquivoPericia()
                    {
                        Id_Lote = id.Value,
                        Nome = hpf.FileName,
                        Tamanho = hpf.ContentLength,
                        Tipo = hpf.ContentType,
                        Path = savedFileName,
                        Usuario = User.Identity.Name
                    };

                    r.Add(ArquivoPericia);

                    RepositorioGlobal.Pericia.InserirArquivo(ArquivoPericia);
                }
            }
            catch (Exception e)
            {
                throw new Exception("caiu aqui");
            }



            if (r.Count > 0)
            {
                var json = JsonConvert.SerializeObject(r);

                return Json(new { ArquivoPericia = r }, JsonRequestBehavior.AllowGet);

                //var res = Content("{\"name\":\"" + r[0].Nome + "\",\"type\":\"" + r[0].Tipo + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Tamanho) + "\"}", "application/json");

                //return new JsonResult();

                //return Content(json, "application/json");
            }
            else
            {
                return Json(new { ArquivoPericia = new ArquivoPericia() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id, string op)
        {
            var lote = RepositorioGlobal.Lote.SelecionarPorId(id);
            
            return View(lote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection, Lote Lote)
        {
            var check = collection["namecheck"];

            try
            {
                // TODO: Add update logic here
                //if (ModelState.IsValid)
                //{
                    RepositorioGlobal.Lote.Alterar(Lote);

                    RepositorioGlobal.Proprietario.Alterar(Lote.Proprietario);

                    return RedirectToAction("Index", new { Id = Lote.id_leilao });
                //}

                //return View(Lote);
            }
            catch
            {
                throw;
            }
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

        [HttpGet]
        public ActionResult SelecionarLotes(Leilao Leilao)
        {
            ViewBag.Leilao = Leilao;
            return View(TempData["arq"]);
        }

        [HttpGet]
        public ActionResult AssociacaoDSIN(int Id)
        {
            ViewBag.Leilao = RepositorioGlobal.Leilao.SelecionarPorId(Id);
            return View();
        }

        [HttpGet]
        public ActionResult ImportacaoDSIN(int Id)
        {
            ViewBag.Leilao = RepositorioGlobal.Leilao.SelecionarPorId(Id);
            ViewBag.QTDE_DIAS = 60;
            ViewBag.QTDE_LOTES = 100;

            return View(new List<REGISTRO>());
        }

        public ActionResult ImportarServicoExterno(int Id)
        {
            return View(RepositorioGlobal.Leilao.SelecionarPorId(Id));
        }

        public PartialViewResult CarregarLotesServicoExterno(int IdLeilao, int NumDias, int NumLotes)
        {
            var leilao = RepositorioGlobal.Leilao.SelecionarPorId(IdLeilao);

            IFormatProvider culture = new System.Globalization.CultureInfo("pt-BR", true);
            DateTime data = Convert.ToDateTime(leilao.data_leilao, culture);
            string dataLeilao = data.ToString("yyyy-MM-dd");

            string depositos = string.Empty;

            foreach (var item in leilao.depositos)
            {
                depositos += item.Id_Deposito.ToString() + ",";
            }

            if (depositos.Length > 0)
                depositos = depositos.Substring(0, depositos.Length - 1);

            var lotes = RepositorioGlobal.Lote.ConsultaDadosLotesLeilao(leilao.descricao);

            var RES = JsonConvert.DeserializeObject<LoteDSIN>(lotes);

            if (RES.COD_RETORNO != "001")
                if (RES.REGISTROS != null)
                {
                    //Inserir Lote
                    RepositorioGlobal.Lote.InserirLoteDSIN(RES, IdLeilao);
                    ViewBag.Msg = "LOTES IMPORTADOS COM SUCESSO.";
                }
                else
                {
                    ViewBag.Erro = "NÃO FORAM ENCONTRADOS LOTES PARA ESTE LEILÃO.";
                }

            ViewBag.IdLeilao = IdLeilao;
            return PartialView("_LotesDSIN", RES);
        }

        public ActionResult ImportarLotesServicoExterno(object LoteDSIN, FormCollection form)
        {
            var lotes = form["lotes"];
            var mLotes = ViewBag.lotes;

            return View();
        }

        public ActionResult ImportarPatios(int Id)
        {
            var leilao = RepositorioGlobal.Leilao.SelecionarPorId(Id);

            ViewBag.clientes = new SelectList(RepositorioGlobal.Cliente.SelecionarTudo(), "Id_Cliente", "Nome");
            ViewBag.depositos = new SelectList(new List<Deposito>(), "Id_Deposito", "Descricao");

            return View(leilao);
        }

        public PartialViewResult ListarGrv(int cliente, int numLotes, int numDias, string data, FormCollection form, string depositos)
        {
            var deps = depositos.Split('_').ToList();
            string depositosVirgula = string.Empty;


            foreach (var item in deps)
            {
                if (item == string.Empty) continue;

                depositosVirgula += "'" + item.ToString() + "',";
            }

            depositosVirgula = depositosVirgula.Substring(0, depositosVirgula.Length - 1);

            //retorna para a partialview os dados conforme filtro

            var teste = RepositorioGlobal.GRV.SelecionarTudo(cliente, depositosVirgula, numLotes, numDias, data: data);

            return PartialView("_ImportarPatios", teste);
        }

        //public string ListarGrv(int cliente, int numLotes, int numDias, string data, FormCollection form, string depositos)
        //{
        //    var deps = depositos.Split('_').ToList();
        //    string depositosVirgula = string.Empty;


        //    foreach (var item in deps)
        //    {
        //        if (item == string.Empty) continue;

        //        depositosVirgula += "'" + item.ToString() + "',";
        //    }

        //    depositosVirgula = depositosVirgula.Substring(0, depositosVirgula.Length - 1);

        //    //retorna para a partialview os dados conforme filtro

        //    var teste = RepositorioGlobal.GRV.SelecionarTudo(cliente, depositosVirgula, numLotes, numDias, data: data);

        //    return JsonConvert.SerializeObject(teste);
        //}

        public JsonResult getDepositosPorCliente(int? idCliente)
        {
            var depositos = RepositorioGlobal.Deposito.SelecionarTudo().Where(d => d.Id_Cliente == idCliente);

            return Json(depositos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IngressarLotes(int Id)
        {
            var leilao = RepositorioGlobal.Leilao.SelecionarPorId(Id);

            if (leilao.comitente.tipo_importacao == 1)   //DSIN
            {
                //LoteRepositorio.DSIN_ConsultaDadosLotes(leilao.Data_Leilao, )

                return RedirectToAction("ImportacaoDSIN", new { Id = Id });
            }

            //if (cr.SelecionarPorId(lr.SelecionarPorId(Id).Id_Comitente).Utiliza_Planilha == 0)
            //{
            //    return RedirectToAction("ImportarPatios", new { Id = Id });
            //}
            //else
            //{
            //    return RedirectToAction("ImportarPlanilha", new { Id = Id });
            //}

            return RedirectToAction("ImportarPatios", new { Id = Id });
        }

        public ActionResult ExcluiArquivo(int? id)
        {
            var r = new List<ArquivoPericia>();
            
            var arquivo = RepositorioGlobal.Pericia.SelecionarArquivosPorID(id.Value);

            System.IO.File.Delete(arquivo.Path);

            RepositorioGlobal.Pericia.ExcluirArquivo(id.Value);

            ViewBag.Msg = "Arquivo Excluído com sucesso.";
            return RedirectToAction("ConferenciaLeilao");
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            List<StatusLote> statusLote = RepositorioGlobal.StatusLote.SelecionarTudo().ToList();
            SelectList ddlStatusLote = new SelectList(statusLote, "Id", "Descricao");
            ViewBag.StatusLote = ddlStatusLote;
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        public void Excel(object Modelo)
        {
            var model = Modelo;

            Export export = new Export();
            export.ToExcel(Response, model);
        }

        public void ExcelXLSX<T>(List<T> Modelo)
        {
            var model = Modelo;

            Export export = new Export();
            export.ToExcel_XLSX(Response, model);
        }

        public JsonResult ConsultaProprietario(int IdLote)
        {
            var jsRes = Json(new { PR = RepositorioGlobal.Proprietario.SelecionarProprietarioPorLote(IdLote) }, JsonRequestBehavior.AllowGet);

            return Json(new { PR = RepositorioGlobal.Proprietario.SelecionarProprietarioPorLote(IdLote) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConsultaCEP(string CEP)
        {
            var MeuCEP = RepositorioGlobal.Util.ConsultaCEP(CEP);

            return Json(new { MeuCEP = MeuCEP }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult teste(FormCollection form)
        {
            var teste = form["id"];
            var idLote = Convert.ToInt32(form["id"]);
            var restricao = form["restricao"];
            var placa = form["placa"];

            RepositorioGlobal.Restricao.Inserir(new Restricao() { restricao = restricao.ToUpper(), id_lote = idLote });

            return RedirectToAction("Edit", new { id = idLote });
        }

        public ActionResult ConferenciaLeilao(string pPlaca, string pChassi, string pProcesso, bool? SalvarDados, Lote model, FormCollection FormCollection, Transacao005 Proprietario, int? id)
        {
            List<StatusLote> statusLote = RepositorioGlobal.StatusLote.SelecionarTudo().ToList();
            SelectList ddlStatusLote = new SelectList(statusLote, "Id", "Descricao");
            ViewBag.StatusLote = ddlStatusLote;

            ViewBag.pPlaca = "";
            ViewBag.pChassi = "";
            ViewBag.pProcesso = "";

            ViewBag.prop = new Proprietario();
            ViewBag.Pericia = new Pericia();
            ViewBag.Restricoes = new List<Restricao>();
            ViewBag.ArquivosPericia = new List<ArquivoPericia>();

            if (SalvarDados != null && SalvarDados.Value)
            {
                //gravar form
                RepositorioGlobal.Lote.Alterar(model);

                Proprietario.Id_lote = model.id;
                Proprietario.Id = 0;

                RepositorioGlobal.Proprietario.Alterar(Proprietario);

                ViewBag.Msg = "Dados gravados com sucesso!";
                ViewBag.prop = RepositorioGlobal.Proprietario.SelecionarProprietarioPorLote(model.id);
                ViewBag.Pericia = RepositorioGlobal.Pericia.SelecionarPorId(model.id);
                ViewBag.Restricoes = RepositorioGlobal.Restricao.SelecionarTudo(model.id);
                ViewBag.Arquivospericia = RepositorioGlobal.Pericia.SelecionarArquivos(model.id);
                
                return View(model);
            }

            try
            {
                Lote filtro = new Lote();

                if (!string.IsNullOrEmpty(pPlaca))
                {
                    filtro = RepositorioGlobal.Lote.ConsultarLote(Placa: pPlaca);
                }

                if (!string.IsNullOrEmpty(pChassi))
                {
                    filtro = RepositorioGlobal.Lote.ConsultarLote(Chassi: pChassi);
                }

                if (!string.IsNullOrEmpty(pProcesso))
                {
                    filtro = RepositorioGlobal.Lote.ConsultarLote(Processo: pProcesso);
                }

                if (id.HasValue)
                {
                    filtro = RepositorioGlobal.Lote.ConsultarLote(IdLote: id.Value);
                }

                ViewBag.pPlaca = pPlaca;
                ViewBag.pChassi = pChassi;
                ViewBag.pProcesso = pProcesso;

                if (filtro == null)
                {
                    ViewBag.erro = "Lote não localizado ou não pertence ao pátio";
                    return View(new Lote());
                }
                else
                {
                    ViewBag.prop = RepositorioGlobal.Proprietario.SelecionarProprietarioPorLote(filtro.id);
                    ViewBag.Pericia = RepositorioGlobal.Pericia.SelecionarArquivosPorID(filtro.id);
                    ViewBag.Restricoes = RepositorioGlobal.Restricao.SelecionarTudo(filtro.id);
                    ViewBag.Leilao = RepositorioGlobal.Leilao.SelecionarPorId(filtro.id_leilao);
                    ViewBag.Arquivospericia = RepositorioGlobal.Pericia.SelecionarArquivos(filtro.id);

                    return View(filtro);
                }

            }
            catch
            {
                throw;
            }
        }

        public ActionResult Ordena(int IdLeilao, string Coluna, FormCollection form)
        {
            return Index(IdLeilao, 1, new Transacao005(), "", "", "", "", "", Ordenacao: Coluna);
        }

        public ActionResult GetFotos(int IdLote)
        {
            var selecao = RepositorioGlobal.FotoRecolhimento.SelecionarTudo(IdLote);

            foreach (var item in selecao)
            {
                var base64 = Convert.ToBase64String(item.foto);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);

                item.linkfoto = imgSrc;
            }

            //Serialização para Json
            var res = Json(selecao);
            return res;
        }

        public ActionResult GetImage()
        {
            string path = Server.MapPath("~/images/dentro21.jpg");
            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image/jpg");
        }

        public ActionResult GetRestricoes(int IdLote)
        {
            return Json(RepositorioGlobal.Restricao.SelecionarTudo(IdLote));
        }

        public ActionResult GetDespesas(int IdLote)
        {
            return Json(RepositorioGlobal.Despesa.SelecionarDespesasLote(IdLote));
        }

        public ActionResult RelPCLote(int IdLote)
        {
            return View(RepositorioGlobal.Lote.RelatorioPrestacaoContasLote(IdLote));
        }

        public void AtribuirNumeroLote(int id)
        {
            RepositorioGlobal.Lote.AtribuirNumeracaoAutomaticaLeilao(id);
        }

        public ActionResult Index(int id, int? page, Transacao005 Proprietario, string placa, string chassi, string marcamodelo, string estadolotes, string numero_formulario_grv, bool ExportarExcel = false, bool LocalizadosFisicamente = false, string Ordenacao = "")
        {
            int TamanhoPagina = 10;
            int NumeroPagina = page ?? 1;

            try
            {
                if (Proprietario.Id_lote > 0)
                {
                    RepositorioGlobal.Proprietario.Alterar(Proprietario);
                }

                List<Lote> filtro = new List<Lote>();
                filtro = RepositorioGlobal.Lote.SelecionarTudo(id).ToList();

                ViewBag.id_leilao = filtro.FirstOrDefault().id_leilao;

                if (filtro.Count > 0)
                {
                    ViewBag.NomeLeilao = filtro.FirstOrDefault().nome_leilao;
                }

                #region WHERE
                if (!string.IsNullOrEmpty(placa))
                {
                    filtro = filtro.Where(p => p.placa.Contains(placa.ToUpper())).ToList();
                }

                if (!string.IsNullOrEmpty(chassi))
                {
                    filtro = filtro.Where(p => p.chassi.Contains(chassi)).ToList();
                }

                if (!string.IsNullOrEmpty(marcamodelo))
                {
                    filtro = filtro.Where(p => p.marca_modelo.Contains(marcamodelo.ToUpper())).ToList();
                }

                if (LocalizadosFisicamente)
                {
                    filtro = filtro.Where(p => p.situacao_lote == "Sim").ToList();
                }

                if (!string.IsNullOrEmpty(estadolotes) && estadolotes != "X")
                {
                    if (estadolotes == "D")
                    {
                        filtro = filtro.Where(l => l.placa != (string.IsNullOrWhiteSpace(l._placa) ? l.placa : l._placa) ||
                                              l.chassi != (string.IsNullOrWhiteSpace(l._chassi) ? l.chassi : l._chassi) ||
                                              l.marca_modelo != (string.IsNullOrWhiteSpace(l._marca_modelo) ? l.marca_modelo : l._marca_modelo) ||
                                              l.tipo_veiculo != (string.IsNullOrWhiteSpace(l._tipo_veiculo) ? l.tipo_veiculo : l._tipo_veiculo) ||
                                              l.cor != (string.IsNullOrWhiteSpace(l._cor) ? l.cor : l._cor)).ToList();
                    }

                    if (estadolotes == "C")
                    {
                        filtro = filtro.Where(l => l.placa == (string.IsNullOrWhiteSpace(l._placa) ? l.placa : l._placa) &&
                                                   l.chassi == (string.IsNullOrWhiteSpace(l._chassi) ? l.chassi : l._chassi) &&
                                                   l.marca_modelo == (string.IsNullOrWhiteSpace(l._marca_modelo) ? l.marca_modelo : l._marca_modelo) &&
                                                   l.tipo_veiculo == (string.IsNullOrWhiteSpace(l._tipo_veiculo) ? l.tipo_veiculo : l._tipo_veiculo) &&
                                                   l.cor == (string.IsNullOrWhiteSpace(l._cor) ? l.cor : l._cor)).ToList();
                    }
                }

                if (!string.IsNullOrEmpty(numero_formulario_grv))
                {
                    filtro = filtro.Where(p => p.numero_formulario_grv.Contains(numero_formulario_grv.ToUpper())).ToList();
                }
                #endregion

                #region ORDER BY
                if (!string.IsNullOrEmpty(Ordenacao))
                {
                    List<Lote> itemList = new List<Lote>();

                    switch (Ordenacao)
                    {
                        case "NumeroLote":

                            itemList = (from t in filtro
                                        orderby t.numero_lote ascending
                                        select t).ToList();

                            filtro = itemList;

                            break;


                        case "Processo":

                            itemList = (from t in filtro
                                        orderby t.numero_formulario_grv ascending
                                        select t).ToList();

                            filtro = itemList;

                            break;

                        case "Placa":

                            itemList = (from t in filtro
                                        orderby t.placa ascending
                                        select t).ToList();

                            filtro = itemList;

                            break;

                        case "Chassi":

                            itemList = (from t in filtro
                                        orderby t.chassi ascending
                                        select t).ToList();

                            filtro = itemList;

                            break;

                        case "MarcaModelo":

                            itemList = (from t in filtro
                                        orderby t.marca_modelo ascending
                                        select t).ToList();

                            filtro = itemList;

                            break;

                        case "Cor":

                            itemList = (from t in filtro
                                        orderby t.cor ascending
                                        select t).ToList();

                            filtro = itemList;

                            break;

                        case "TipoVeiculo":

                            itemList = (from t in filtro
                                        orderby t.tipo_veiculo ascending
                                        select t).ToList();

                            filtro = itemList;

                            break;

                        default:
                            break;
                    }
                }
                #endregion

                ViewBag.placa = placa;
                ViewBag.chassi = chassi;
                ViewBag.marcamodelo = marcamodelo;
                ViewBag.EstadoLotes = estadolotes;
                ViewBag.LocalizadosFisicamente = LocalizadosFisicamente;
                ViewBag.numero_formulario_grv = numero_formulario_grv;
                ViewBag.qtdregistros = filtro.Count;

                if (filtro.Count <= 0)
                {
                    ViewBag.Erro = "LOTE NÃO ENCONTRADO OU NÃO PERTENCE AO LEILÃO.";
                }

                if (ExportarExcel == true)
                    ExcelXLSX(filtro);

                return View(filtro.ToPagedList(NumeroPagina, TamanhoPagina));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        struct Despesa
        {
            public int IdDespesa { get; set; }
            public string Valor { get; set; }
        }

        public ActionResult GravarDespesas(string valores, string selecionados, int id_leilao, FormCollection form)
        {
            var lotes = selecionados.Split(',');
            var despesas = JsonConvert.DeserializeObject<List<Despesa>>(valores);
            
            foreach (var l in lotes)
            {
                foreach (var d in despesas)
                {
                    Despesa_Lote dl = new Despesa_Lote()
                    {
                        Id_Despesa = d.IdDespesa,
                        Id_Lote = Convert.ToInt32(l),
                        Valor = Convert.ToDouble(d.Valor)
                    };

                    RepositorioGlobal.Despesa.InserirDespesaLote(dl);
                }
            }

            return RedirectToAction("Index", new { Id = id_leilao });
        }

        public ActionResult ImportarPlanilha(int Id)
        {
            var leilao = RepositorioGlobal.Leilao.SelecionarPorId(Id);
            return View(leilao);
        }

        public ActionResult AgendaLote(int id)
        {
            var lote = RepositorioGlobal.Lote.SelecionarPorId(id);

            lote.Flag_Agendado = "S";

            RepositorioGlobal.Lote.Alterar(lote);

            var res = Json("OK");
            return res;
        }       
    }
}
