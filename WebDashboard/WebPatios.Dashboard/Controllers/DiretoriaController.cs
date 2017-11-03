using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using WebPatios.Business;

namespace WebPatios.Dashboard.Controllers
{
    public class DiretoriaController : Controller
    {
        // GET: Diretoria
        public ActionResult Index()
        {
            ViewBag.Title = "Diretoria";

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
            var ent = new Business.DiretoriaBLL(filtro);

            return View();
        }

        public ActionResult EvolucaoMensalPorEstado()
        {
            //List<EvolucaoPorEstadoModel> dados = new List<EvolucaoPorEstadoModel>()
            //{
            //    new EvolucaoPorEstadoModel() { ANO = 2016, FATURAMENTO = 58970, MES = 12, UF = "BA" },
            //    new EvolucaoPorEstadoModel() { ANO = 2016, FATURAMENTO = 4387.37, MES = 11, UF = "PB" },
            //    new EvolucaoPorEstadoModel() { ANO = 2016, FATURAMENTO = 97664.24, MES = 12, UF = "PB" },
            //    new EvolucaoPorEstadoModel() { ANO = 2017, FATURAMENTO = 86767, MES = 1, UF = "BA" },
            //    new EvolucaoPorEstadoModel() { ANO = 2017, FATURAMENTO = 98214.5, MES = 2, UF = "BA" }
            //};

            var chart = new System.Web.UI.DataVisualization.Charting.Chart();

            chart.Width = 1000;          //LARGURA
            chart.Height = 1000;         //ALTURA

            chart.BackColor = Color.FromArgb(211, 223, 240);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 1;
            //chart.Palette = ChartColorPalette.BrightPastel;
            chart.Palette = ChartColorPalette.SeaGreen;
            chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            chart.RenderType = RenderType.BinaryStreaming;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
            chart.Titles.Add(CreateTitle());
            chart.Legends.Add(CreateLegend());
            chart.AlignDataPointsByAxisLabel();


            var dados = new DiretoriaBLL().GraficoEvolucaoMensalEstado();

            //var dados1 = (from X in dados orderby X.ANO, X.MES group X by X.UF into N select N).ToList();
            var dados1 = (from X in dados orderby X.ANO descending, X.MES descending group X by X.UF into N select N).ToList();

            foreach (var item in dados1)
            {
                chart.Series.Add(CriarSerie(SeriesChartType.Bar, item.ToList()));
            }


            chart.ChartAreas.Add(CreateChartArea());
            chart.DataSource = dados;

            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms);

            return File(ms.GetBuffer(), @"image/png");
        }

        public ActionResult EvolucaoMensalPorPatio()
        {
            ViewBag.Title = "Diretoria";

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
            var ent = new Business.DiretoriaBLL(filtro);

            return View();
        }

        [HttpPost]
        public ActionResult EvolucaoMensalPorPatio(FormCollection form)
        {

            var id = form["IDLEILAO"];


            return View("EvolucaoMensalPatioGrafico");
        }

        public ActionResult EvolucaoMensalPorPatio1(FormCollection form, string id)
        {
            var chart = new System.Web.UI.DataVisualization.Charting.Chart();

            chart.Width = 1000;          //LARGURA
            chart.Height = 1000;         //ALTURA

            chart.BackColor = Color.FromArgb(211, 223, 240);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BackSecondaryColor = Color.White;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 1;
            chart.Palette = ChartColorPalette.BrightPastel;
            //chart.Palette = ChartColorPalette.SeaGreen;
            chart.BorderlineColor = Color.FromArgb(26, 59, 105);
            chart.RenderType = RenderType.BinaryStreaming;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
            chart.Titles.Add(CreateTitlePatio());
            chart.Legends.Add(CreateLegend());
            chart.AlignDataPointsByAxisLabel();


            var dados = new DiretoriaBLL().GraficoEvolucaoMensalPatio();

            //var dados1 = (from X in dados orderby X.ANO, X.MES group X by X.UF into N select N).ToList();
            var dados1 = (from X in dados orderby X.ANO descending, X.MES descending group X by X.FATURAMENTO into N select N).ToList();

            foreach (var item in dados1)
            {
                chart.Series.Add(CriarSeriePatio(SeriesChartType.Bar, item.ToList()));
            }


            chart.ChartAreas.Add(CreateChartArea());
            chart.DataSource = dados;

            MemoryStream ms = new MemoryStream();
            chart.SaveImage(ms);

            return File(ms.GetBuffer(), @"image/png");
        }

        public Series CriarSerie(SeriesChartType chartType, ICollection<DiretoriaBLL.EvolucaoPorEstadoModel> list)
        {
            var series = new Series
            {
                Name = list.FirstOrDefault().UF,
                IsValueShownAsLabel = true,
                //Color = Color.FromArgb(198, 99, 99),
                ChartType = chartType,
                BorderWidth = 2,
                //YValueType = ChartValueType.String,
                //YValueMembers = "Number"
            };

            foreach (var item in list)
            {
                var point = new DataPoint
                {
                    AxisLabel = item.ANO.ToString() + "/" + item.MES.ToString(),
                    YValues = new double[] { item.FATURAMENTO },
                    //XValue = Posicao_Eixo_X,
                    Label = item.FATURAMENTO.ToString("C"),
                    //LabelAngle = -45
                };

                series.Points.Add(point);
            }

            return series;
        }

        public Series CriarSeriePatio(SeriesChartType chartType, ICollection<DiretoriaBLL.EvolucaoPorPatio> list)
        {
            var series = new Series
            {
                Name = list.FirstOrDefault().PATIO,
                IsValueShownAsLabel = true,
                //Color = Color.FromArgb(198, 99, 99),
                ChartType = chartType,
                BorderWidth = 1,
                //YValueType = ChartValueType.String,
                //YValueMembers = "Number"

            };

            foreach (var item in list)
            {
                var point = new DataPoint
                {
                    AxisLabel = item.ANO.ToString() + "/" + item.MES.ToString(),
                    YValues = new double[] { item.FATURAMENTO },
                    //XValue = Posicao_Eixo_X,
                    Label = item.PATIO + " - " + item.FATURAMENTO.ToString("C"),
                    //LabelAngle = -45
                };

                series.Points.Add(point);
            }

            return series;
        }

        public Title CreateTitle()
        {
            Title title = new Title
            {
                Text = "EVOLUÇÃO MENSAL POR ESTADO",
                ShadowColor = Color.FromArgb(32, 0, 0, 0),
                Font = new Font("Trebuchet MS", 14F, FontStyle.Bold),
                ShadowOffset = 3,
                ForeColor = Color.FromArgb(26, 59, 105)

            };

            return title;
        }

        public Title CreateTitlePatio()
        {
            Title title = new Title
            {
                Text = "EVOLUÇÃO MENSAL POR ESTADO",
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
                //chartArea.Name = "GDP Current Prices in Billion($)";

                BackColor = Color.Transparent,

                AxisX = new Axis()
                {
                    IsLabelAutoFit = false,
                    Interval = 1,

                    LabelStyle = new LabelStyle()
                    {
                        Font = new Font("Verdana,Arial,Helvetica,sans-serif", 8F, FontStyle.Bold)
                    },

                    MajorGrid = new Grid()
                    {
                        LineColor = Color.AliceBlue
                    }

                    //chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
                    //chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
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
                        //chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
                        LineColor = Color.AliceBlue
                    }
                },

                //AxisY2 = new Axis()
                //{
                //    IsReversed = true
                //}
            };

            return chartArea;
        }

        public Legend CreateLegend()
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