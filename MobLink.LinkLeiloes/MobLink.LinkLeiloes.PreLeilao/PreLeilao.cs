using MobLink.WebLeilao.Repositorio;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

/* 1-CONSULTA VEÍCULO
 * 2-ALTERAÇÃO PARA STATUS LOTEAMENTO/SERVIÇOS
 * 3-INCLUSÃO VEÍCULOS PATIO E CONSULTA VEÍCULOS PARA LEILÃO
*/

namespace MobLink.WebLeilao.PreLeilao
{
    partial class PreLeilao : ServiceBase
    {
        private Timer timer;

        public PreLeilao()
        {
            //InitializeComponent();
            this.ServiceName = "PreLeilao";
        }

        protected override void OnStart(string[] args)
        {
            Debugger.Launch();

            timer = new Timer(10000D);    //10000 milliseconds = 10 seconds
                                          //timer = new Timer(1800000D);  // 1800000 milliseconds = 30 minutes
                                          //timer = new Timer(5000D)         // 5000 milliseconds = 5 seconds
            timer.AutoReset = true;

            timer.Elapsed += new ElapsedEventHandler(this.timer_Elapsed);
            timer.Start();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer.Stop();

                RepositorioGlobal.Transacoes.Consulta();

                RepositorioGlobal.Transacoes.Acautelamento();

                RepositorioGlobal.Transacoes.Proprietarios();

                RepositorioGlobal.Transacoes.Normalizacao();
            }
            catch (Exception ex)
            {
                timer.Start();
            }
            finally
            {
                timer.Start();
            }
        }

        protected override void OnStop()
        {
            this.timer.Stop();
            this.timer = null;
        }
    }
}
