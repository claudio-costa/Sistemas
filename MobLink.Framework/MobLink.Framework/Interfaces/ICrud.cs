using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.Framework.Interfaces
{
    public interface ICrud<TEntidade, TChave> where TEntidade : class
    {
        IList<TEntidade> SelecionarTudo();
        IList<TEntidade> SelecionarTudo(TEntidade Entidade);
        IList<TEntidade> SelecionarTudo(int id);

        TEntidade SelecionarPorId(TChave id);

        int Inserir(TEntidade Entidade);
        int Alterar(TEntidade Entidade);
        int Excluir(TChave id);
        int Excluir(TEntidade Entidade);
    }
}
