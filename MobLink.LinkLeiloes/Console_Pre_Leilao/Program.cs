using MobLink.WebLeilao.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pre_Leilao
{
    class Program
    {
        static void Main(string[] args)
        {
            RepositorioGlobal.Transacoes.Consulta();
            
            RepositorioGlobal.Transacoes.Acautelamento();
            
            RepositorioGlobal.Transacoes.Proprietarios();
            
            RepositorioGlobal.Transacoes.Normalizacao();
        }
    }
}
