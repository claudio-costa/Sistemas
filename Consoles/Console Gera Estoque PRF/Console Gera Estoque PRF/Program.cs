using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Repositorio r = new Repositorio();

            var tabela = r.PegarEstoque(9);

            tabela.Columns.Add("restricoes");

            foreach (System.Data.DataRow item in tabela.Rows)
            {
                var placa = item["placa"].ToString();

                Console.WriteLine(placa + "-" + item["chassi"].ToString());

                var rest = r.PegarRestricoes(placa);

                if (rest != null)
                {

                }

                //tabela.Rows[item]["restricoes"] = rest;

                item["restricoes"] = rest;

                tabela.AcceptChanges();
            }
        }
    }
}
