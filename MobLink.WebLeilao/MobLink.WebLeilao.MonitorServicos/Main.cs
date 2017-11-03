using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobLink.WebLeilao.MonitorServicos
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            serviceController1.MachineName = ".";
            serviceController1.ServiceName = "MobLink - Pré Leilão";
            textBox1.Text = serviceController1.Status.ToString().ToUpper();

            //System.ServiceProcess.ServiceController svc = new System.ServiceProcess.ServiceController("MobLink - Pré Leilão", "179.107.47.91");

            //textBox4.Text = svc.Status.ToString().ToUpper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string svcPath;
            string svcName;
            string svcDispName;


            svcPath = @"C:\Users\Claudio Costa\OneDrive\Clientes\Mob-Link\Sistemas\WebLeilao\MobLink.WebLeilao\MobLink.WebLeilao.PreLeilao\bin\Debug\MobLink.WebLeilao.PreLeilao.exe";
            svcDispName = "MobLink - Pré Leilão - Display";
            svcName = "MobLink - Pré Leilão";

            //ServiceInstaller c = new ServiceInstaller();

            SvcInstall svcvv = new SvcInstall();
            var res = svcvv.UnInstallService(svcName);

            Console.Read();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string svcPath;
            string svcName;
            string svcDispName;

            
            svcPath = @"C:\Users\Claudio Costa\OneDrive\Clientes\Mob-Link\Sistemas\WebLeilao\MobLink.WebLeilao\MobLink.WebLeilao.PreLeilao\bin\Debug\MobLink.WebLeilao.PreLeilao.exe";
            svcDispName = "MobLink - Pré Leilão - Display";
            svcName = "MobLink - Pré Leilão";
            
            //ServiceInstaller c = new ServiceInstaller();

            SvcInstall svcvv = new SvcInstall();
            var res = svcvv.InstallService(svcPath, svcName, svcDispName);
            
            Console.Read();
        }
    }
}
