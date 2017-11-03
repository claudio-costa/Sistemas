using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.PosLeilao
{
    partial class PosLeilao : ServiceBase
    {
        public PosLeilao()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Adicione aqui o código para iniciar seu serviço.
        }

        protected override void OnStop()
        {
            // TODO: Adicione aqui o código para realizar qualquer desmontagem necessária para interromper seu serviço.
        }
    }
}
