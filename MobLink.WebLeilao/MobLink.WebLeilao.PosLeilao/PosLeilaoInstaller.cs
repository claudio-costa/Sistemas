using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.PosLeilao
{
    [RunInstaller(true)]
    public partial class PosLeilaoInstaller : System.Configuration.Install.Installer
    {
        public PosLeilaoInstaller()
        {
            InitializeComponent();
        }
    }
}
