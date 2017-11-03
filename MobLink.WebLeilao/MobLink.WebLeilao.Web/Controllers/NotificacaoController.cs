using MobLink.WebLeilao.Dominio;
using MobLink.WebLeilao.Repositorio;
using MobLink.WebLeilao.Web.Models;
using MobLink.WebLeilao.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MobLink.WebLeilao.Web.Controllers
{
    [Authorize]
    public class NotificacaoController : Controller
    {
        public ActionResult Index()
        {
            //var leiloes = RepositorioGlobal.Leilao.SelecionarTudo();
            //var status = RepositorioGlobal.StatusLote.SelecionarTudo();

            //ViewBag.Leiloes = new SelectList(leiloes, "id", "Descricao", null);
            //ViewBag.StatusLotes = new SelectList(status, "id", "descricao", null);

            //RepositorioGlobal.Notificacao.SelecionarTudo

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var TipoNotificacao = form["TIPO_NOTIFICACAO"];






            var IdLeilao = form["IDLEILAO"];

            if (TipoNotificacao == "" && IdLeilao == "")
            {
                TempData["ERRO"] = "É NECESSÁRIO ESCOLHER TIPO DE NOTIFICAÇÃO E LEILÃO PARA PROSSEGUIR";
                return RedirectToAction("Index");
            }

            var StatusComCheck = form["CHK"];

            bool gerarArquivo = false;

            gerarArquivo = bool.TryParse(form["GERAR"], out gerarArquivo);

            if (gerarArquivo)
            {
                //GetTextFile(IdLeilao, StatusComCheck);
            }

            //var leiloes = RepositorioGlobal.Leilao.SelecionarTudo();
            //var status = RepositorioGlobal.StatusLote.SelecionarTudo();

            //ViewBag.Leiloes = new SelectList(leiloes, "id", "Descricao", IdLeilao);
            //ViewBag.StatusLotes = new SelectList(status, "id", "descricao", StatusComCheck);

            ViewBag.Proprietarios = RepositorioGlobal.Proprietario.SelecionarTudoLeilao(Convert.ToInt32(IdLeilao));

            var proprietarios = RepositorioGlobal.Proprietario.SelecionarTudoLeilao(Convert.ToInt32(IdLeilao)).ToList();

            List<Proprietario> Proprietarios_Notificaveis = new List<Proprietario>();
            List<Proprietario> Proprietarios_Nao_Notificaveis = new List<Proprietario>();

            List<Proprietario> ComunicadoVenda_Notificaveis = new List<Proprietario>();
            List<Proprietario> ComunicadoVenda_Nao_Notificaveis = new List<Proprietario>();

            List<Proprietario> Financeiras_Notificaveis = new List<Proprietario>();
            List<Proprietario> Financeiras_Nao_Notificaveis = new List<Proprietario>();

            //foreach (var p in proprietarios)
            //{
            //    if (!string.IsNullOrEmpty(p.numero_documento) &&
            //        !string.IsNullOrEmpty(p.cep) &&
            //        !string.IsNullOrEmpty(p.nome) &&
            //        !string.IsNullOrEmpty(p.endereco) &&
            //        !string.IsNullOrEmpty(p.numero) &&
            //        !string.IsNullOrEmpty(p.complemento) &&
            //        !string.IsNullOrEmpty(p.bairro) &&
            //        !string.IsNullOrEmpty(p.uf) &&
            //        !string.IsNullOrEmpty(p.municipio) &&
            //        p.flag_notificar_proprietario == "S")
            //    {
            //        //PROPRIETARIO NOTIFICAVEL
            //        Proprietarios_Notificaveis.Add(p);
            //    }
            //    else
            //    {
            //        //PROPRIETARIO NÃO NOTIFICAVEL
            //        Proprietarios_Nao_Notificaveis.Add(p);
            //    }

            //    if (!string.IsNullOrEmpty(p.tipo_documento_comunicado_venda) &&
            //        !string.IsNullOrEmpty(p.cpf_cnpj_comunicado_venda) &&
            //        !string.IsNullOrEmpty(p.cep_comunicado_venda) &&
            //        !string.IsNullOrEmpty(p.nome_comunicado_venda) &&
            //        !string.IsNullOrEmpty(p.endereco_comunicado_venda) &&
            //        !string.IsNullOrEmpty(p.numero_comunicado_venda) &&
            //        !string.IsNullOrEmpty(p.complemento_comunicado_venda) &&
            //        !string.IsNullOrEmpty(p.bairro_comunicado_venda) &&
            //        !string.IsNullOrEmpty(p.uf_comunicado_venda) &&
            //        !string.IsNullOrEmpty(p.municipio_comunicado_venda) &&
            //        p.flag_notificar_comunicado == "S")
            //    {
            //        //COMUNICADO VENDA NOTIFICAVEL
            //        ComunicadoVenda_Notificaveis.Add(p);
            //        p.Comunicado_Conferido = true;
            //    }
            //    else
            //    {
            //        //COMUNICADO VENDA NÃO NOTIFICAVEL
            //        ComunicadoVenda_Nao_Notificaveis.Add(p);
            //        p.Comunicado_Conferido = false;
            //    }

            //    if (!string.IsNullOrEmpty(p.tipo_documento_financiamento_efet) &&
            //        !string.IsNullOrEmpty(p.cpf_cnpj_financiamento_efet) &&
            //        !string.IsNullOrEmpty(p.cep_financiamento_efet) &&
            //        !string.IsNullOrEmpty(p.nome_financiamento_efet) &&
            //        !string.IsNullOrEmpty(p.endereco_financiamento_efet) &&
            //        !string.IsNullOrEmpty(p.numero_financiamento_efet) &&
            //        !string.IsNullOrEmpty(p.complemento_financiamento_efet) &&
            //        !string.IsNullOrEmpty(p.bairro_financiamento_efet) &&
            //        !string.IsNullOrEmpty(p.uf_financiamento_efet) &&
            //        !string.IsNullOrEmpty(p.municipio_financiamento_efet) &&
            //        p.flag_notificar_financeira == "S")
            //    {
            //        //FINANCEIRA NOTIFICAVEL
            //        Financeiras_Notificaveis.Add(p);
            //        p.Financeira_Conferido = true;
            //    }
            //    else
            //    {
            //        //FINANCEIRA NÃO NOTIFICAVEL
            //        Financeiras_Nao_Notificaveis.Add(p);
            //        p.Financeira_Conferido = false;
            //    }
            //}


            var conjunto = Proprietarios_Notificaveis.Union(ComunicadoVenda_Notificaveis).Union(Financeiras_Notificaveis).ToList<Proprietario>();
            //var conjuntoNotificados = proprietarios.Where(x => x.Proprietario_Conferido == true || x.Comunicado_Conferido == true || x.Financeira_Conferido == true).ToList();
            //conjuntoNotificados.OrderBy(x => x.id);

            //proprietarios = conjuntoNotificados;

            if (proprietarios.Count == 0)
            {
                ViewBag.Erro = "Não foram encontrados proprietários, arrendatários ou comunicação de venda cadastrados nos lotes e condições selecionadas!";
            }

            return View(proprietarios);
        }

        private List<ProprietarioSPE> RetornaProprietariosValidos(Transacao005 p)
        {
            List<ProprietarioSPE> listaRetorno = new List<ProprietarioSPE>();

            //if (!string.IsNullOrEmpty(p.nome_proprietario) &
            //    !string.IsNullOrEmpty(p.endereco_proprietario) &
            //    !string.IsNullOrEmpty(p.numero_endereco_proprietario) &
            //    !string.IsNullOrEmpty(p.complemento_endereco_proprietario) &
            //    !string.IsNullOrEmpty(p.descricao_municipio_endereco) &
            //    !string.IsNullOrEmpty(p.cep_endereco_proprietario) &
            //    !string.IsNullOrEmpty(p.bairro_proprietario) &
            //    !string.IsNullOrEmpty(p.uf_proprietario) & p.flag_notificar_proprietario == "S")
            //{
            //    //PROPRIETARIO
            //    listaRetorno.Add(new ProprietarioSPE()
            //    {
            //        BAIRRO = p.bairro_proprietario,
            //        CEP = p.cep_endereco_proprietario,
            //        CHASSI = p.chassi,
            //        COMPLEMENTO = p.complemento_endereco_proprietario,
            //        DATA_HORA_APREENSAO = p.Data_Hora_Apreensao,
            //        ENDERECO = p.endereco_proprietario,
            //        FORMULARIO_GRV = p.Processo,
            //        MARCA_MODELO = p.descricao_marca,
            //        MUNICIPIO = p.descricao_municipio_endereco,
            //        NOME = p.nome_proprietario,
            //        NUMERO = p.numero_endereco_proprietario,
            //        PLACA = p.placa,
            //        RENAVAM = p.renavam,
            //        UF = p.uf_proprietario
            //    });
            //}

            //if (!string.IsNullOrEmpty(p.nome_comunicado_venda) &
            //    !string.IsNullOrEmpty(p.endereco_comunicado_venda) &
            //    !string.IsNullOrEmpty(p.numero_comunicado_venda) &
            //    !string.IsNullOrEmpty(p.complemento_comunicado_venda) &
            //    !string.IsNullOrEmpty(p.municipio_comunicado_venda) &
            //    !string.IsNullOrEmpty(p.cep_comunicado_venda) &
            //    !string.IsNullOrEmpty(p.bairro_comunicado_venda) &
            //    !string.IsNullOrEmpty(p.uf_comunicado_venda) & p.flag_notificar_comunicado == "S")
            //{
            //    //COMUNICADO DE VENDA
            //    listaRetorno.Add(new ProprietarioSPE()
            //    {
            //        BAIRRO = p.bairro_comunicado_venda,
            //        CEP = p.cep_comunicado_venda,
            //        CHASSI = p.chassi,
            //        COMPLEMENTO = p.complemento_comunicado_venda,
            //        DATA_HORA_APREENSAO = p.Data_Hora_Apreensao,
            //        ENDERECO = p.endereco_comunicado_venda,
            //        FORMULARIO_GRV = p.Processo,
            //        MARCA_MODELO = p.descricao_marca,
            //        MUNICIPIO = p.municipio_comunicado_venda,
            //        NOME = p.nome_comunicado_venda,
            //        NUMERO = p.numero_comunicado_venda,
            //        PLACA = p.placa,
            //        RENAVAM = p.renavam,
            //        UF = p.uf_comunicado_venda
            //    });
            //}

            //if (!string.IsNullOrEmpty(p.nome_financiamento_efet) &
            //    !string.IsNullOrEmpty(p.endereco_financiamento_efet) &
            //    !string.IsNullOrEmpty(p.numero_financiamento_efet) &
            //    !string.IsNullOrEmpty(p.complemento_financiamento_efet) &
            //    !string.IsNullOrEmpty(p.municipio_financiamento_efet) &
            //    !string.IsNullOrEmpty(p.cep_financiamento_efet) &
            //    !string.IsNullOrEmpty(p.bairro_financiamento_efet) &
            //    !string.IsNullOrEmpty(p.uf_financiamento_efet) & p.flag_notificar_financeira == "S")
            //{
            //    //FINANCEIRA
            //    listaRetorno.Add(new ProprietarioSPE()
            //    {
            //        BAIRRO = p.bairro_financiamento_efet,
            //        CEP = p.cep_financiamento_efet,
            //        CHASSI = p.chassi,
            //        COMPLEMENTO = p.complemento_financiamento_efet,
            //        DATA_HORA_APREENSAO = p.Data_Hora_Apreensao,
            //        ENDERECO = p.endereco_financiamento_efet,
            //        FORMULARIO_GRV = p.Processo,
            //        MARCA_MODELO = p.descricao_marca,
            //        MUNICIPIO = p.municipio_financiamento_efet,
            //        NOME = p.nome_financiamento_efet,
            //        NUMERO = p.numero_financiamento_efet,
            //        PLACA = p.placa,
            //        RENAVAM = p.renavam,
            //        UF = p.uf_financiamento_efet
            //    });
            //}

            return listaRetorno;
        }


        [HttpGet]
        public JsonResult getNotificacoes()
        {
            var notificacoes = RepositorioGlobal.Notificacao.SelecionarTudo();
            return Json(notificacoes, JsonRequestBehavior.AllowGet);
        }
    }
}