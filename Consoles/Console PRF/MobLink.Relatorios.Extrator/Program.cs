using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.Relatorios.Extrator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio do processo: " + DateTime.Now.ToString());

            Negocio.PRFRepositorio PRFRepositorio = new Negocio.PRFRepositorio();

            PRFRepositorio.GravarRelatorioPRF();

            Console.WriteLine("Fim do processo: " + DateTime.Now.ToString());

            Console.ReadLine();
        }
    }
}
