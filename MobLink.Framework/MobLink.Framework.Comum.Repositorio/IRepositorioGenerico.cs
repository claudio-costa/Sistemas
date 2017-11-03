using System.Collections.Generic;

namespace MobLink.Framework.Repositorio
{
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public interface IRepositorioGenerico<TEntidade, TChave> where TEntidade : class
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
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
}
