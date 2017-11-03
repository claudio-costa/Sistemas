using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WebLeilao.PreLeilao
{
    public static class Program
    {
        public static void Main()
        {
            System.Diagnostics.Debugger.Launch();
            
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            ServiceBase[] ServicesToRun = new ServiceBase[]
            {
                new PreLeilao()
            };

            ServiceBase.Run(ServicesToRun);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
