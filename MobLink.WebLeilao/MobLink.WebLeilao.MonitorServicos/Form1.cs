using System;
using System.Windows.Forms;

namespace MobLink.WebLeilao.MonitorServicos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Repositorio.RepositorioGlobal.Transacoes.ConsultarVeiculo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Repositorio.RepositorioGlobal.Transacoes.InclusaoVeiculoPatio();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Repositorio.RepositorioGlobal.Transacoes.ConsultarVeiculoParaLeilao();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var status = serviceController1.Status;

            //ServiceController svc = new ServiceController();

            //svc.ServiceName = "MobLink - Pré Leilão";
            //svc.MachineName = ".";

            //MessageBox.Show(svc.Status.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Repositorio.RepositorioGlobal.Transacoes.ComplementarProprietario();
        }
        
    }
}
