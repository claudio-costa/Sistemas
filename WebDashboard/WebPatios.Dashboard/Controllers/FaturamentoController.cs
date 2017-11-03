using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using WebPatios.Business;
using static WebPatios.Business.FaturamentoBLL;

namespace WebPatios.Dashboard.Controllers
{
    public class FaturamentoController : Controller
    {
        // GET: Faturamento
        public ActionResult Index()
        {
            ViewBag.Title = "Faturamento";

            //ViewBag.Title = "Faturamento";

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


            var ent = new Business.FaturamentoBLL(filtro);
            //var dados = ent.GetDadosGrafico();

            return View();
        }

        public ActionResult Chart()
        {


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

            var ent = new Business.FaturamentoBLL(filtro);
            var dados = ent.GetDadosGrafico();

            return View(dados);
        }

        public ActionResult GerarGraficoFaturamentoPorDeposito()
        {
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

            #region PARAMETRIZAÇÃO DO GRÁFICO
            var chart = new System.Web.UI.DataVisualization.Charting.Chart();
            chart.Width = 900;          //LARGURA
            chart.Height = 500;         //ALTURA

            chart.BackColor = Color.FromArgb(211, 223, 240);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 1;
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            chart.RenderType = RenderType.BinaryStreaming;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
            chart.Titles.Add(CreateTitle(filtro.clienteDeposito.nomeCliente));
            chart.Legends.Add(CreateLegend());
            #endregion


            //CONSULTA DOS DADOS
            var ent = new Business.FaturamentoBLL(filtro);
            var dados = ent.GetDadosGrafico();
            var graf = ent.GraficoFaturamento();

            #region SELECIONAR DISTINTAMENTE EIXO-X E RÓTULOS DOS MESMOS
            var Eixos = graf.Select(x => x.CoordenadasGrafico).Distinct().Select(c => c.FirstOrDefault().ROTULO_X).Distinct();

            Dictionary<string, int> ColecaoEixos = new Dictionary<string, int>();

            int count = 1;

            foreach (var item in Eixos)
            {
                ColecaoEixos.Add(item.ToString(), count);
                count++;
            }
            #endregion

            foreach (var item in graf)
            {
                var Deposito = item.Deposito;
                Coordenadas C = item.CoordenadasGrafico.FirstOrDefault();
                C.X = ColecaoEixos.Where(e => e.Key == C.ROTULO_X).FirstOrDefault().Value;

                try
                {
                    //TENTO CRIAR A SÉRIE E ADICIONAR COORDENADA, CASO JA EXISTA ADICIONAR COORDENADA (CATCH)
                    chart.Series.Add(CreateSeries(SeriesChartType.RangeColumn, C.ROTULO_X, C.X, C.Y, Deposito.descricaoDeposito));
                }
                catch
                {
                    chart.Series[Deposito.descricaoDeposito].Points.Add(new DataPoint()
                    {
                        AxisLabel = C.ROTULO_X,
                        YValues = new double[] { C.Y },
                        XValue = C.X,
                        Label = C.Y.ToString("C"),
                        //LabelAngle = -90
                    });
                }
            }

            chart.ChartAreas.Add(CreateChartArea());

            //EXEMPLO USANDO UM DATASOURCE
            //var statusNumbers = new Dictionary<string, string>
            //{
            //    new StatusNumber{Number="55555", Status="USA"},
            //    new StatusNumber{Number="11385", Status="China"},
            //    new StatusNumber{Number="4116", Status="Japan"},
            //    new StatusNumber{Number="3371", Status="Germany"},
            //    new StatusNumber{Number="2865", Status="UK"},
            //    new StatusNumber{Number="2423", Status="France"},
            //    new StatusNumber{Number="2183", Status="India"},
            //};
            //chart.DataSource = pontos;

            MemoryStream ms = new MemoryStream();

            chart.SaveImage(ms);

            return File(ms.GetBuffer(), @"image/png");
        }

        private Series CreateSeries(SeriesChartType chartType, string Rotulo_X, double Posicao_Eixo_X, double Valor_Eixo_Y, string NomeSerie)
        {
            var series = new Series
            {
                Name = NomeSerie,
                IsValueShownAsLabel = true,
                //Color = Color.FromArgb(198, 99, 99),          //DETERMINA COR ÚNICA. QUANDO COMENTADO É ALEATÓRIO
                ChartType = chartType,
                BorderWidth = 2
                //YValueType = ChartValueType.String,
                //YValueMembers = "Number"
                //, YValueType = ChartValueType.String, LabelFormat="C"
            };

            var point = new DataPoint
            {
                AxisLabel = Rotulo_X,
                YValues = new double[] { Valor_Eixo_Y },
                XValue = Posicao_Eixo_X,
                Label = Valor_Eixo_Y.ToString("C"),
                //LabelAngle = -90
            };

            series.Points.Add(point);

            return series;
        }

        private Title CreateTitle(string Titulo)
        {
            Title title = new Title
            {
                Text = Titulo,
                ShadowColor = Color.FromArgb(32, 0, 0, 0),
                Font = new Font("Trebuchet MS", 14F, FontStyle.Bold),
                ShadowOffset = 3,
                ForeColor = Color.FromArgb(26, 59, 105)
            };

            return title;
        }

        public ChartArea CreateChartArea()
        {
            var chartArea = new ChartArea()
            {
                //Name = "GDP Current Prices in Billion($)";

                BackColor = Color.Transparent,

                AxisX = new Axis()
                {
                    IsLabelAutoFit = false,
                    Interval = 1,

                    LabelStyle = new LabelStyle()
                    {
                        Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular)
                    },

                    LineColor = Color.FromArgb(64, 64, 64, 64),

                    MajorGrid = new Grid()
                    {
                        LineColor = Color.FromArgb(64, 64, 64, 64)
                    }
                },

                AxisY = new Axis()
                {
                    IsLabelAutoFit = false,
                    LineColor = Color.FromArgb(64, 64, 64, 64),

                    LabelStyle = new LabelStyle()
                    {
                        Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Regular),
                        Format = "C"
                    },

                    MajorGrid = new Grid()
                    {
                        LineColor = Color.FromArgb(64, 64, 64, 64)
                    }

                    //chartArea.AxisY.LabelStyle.Format = "{0:0,0.00}";
                },

                //AxisY2 = new Axis()
                //{
                //    IsReversed = true
                //}
            };

            return chartArea;
        }

        private Legend CreateLegend()
        {
            var legend = new Legend
            {
                //Name = "GDP Current Prices in Billion($)",
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                BackColor = Color.Transparent,
                Font = new Font(new FontFamily("Trebuchet MS"), 9),
                LegendStyle = LegendStyle.Table
            };

            return legend;
        }
    }
}