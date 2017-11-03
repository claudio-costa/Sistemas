using MobLink.Framework;
using MobLink.Framework.CoreBusiness;
using MobLink.Framework.Database;
using MobLink.Framework.Interfaces;
using MobLink.WebLeilao.Dominio;
using System;
using System.Collections.Generic;

namespace MobLink.WebLeilao.Repositorio
{
    public class ComitenteRepositorio : DbSqlServer, ICrud<Comitente, int>
    {
        protected internal ComitenteRepositorio() : base(Util.DetectarConexao())
        {

        }

        public int Alterar(Comitente Entidade)
        {
            throw new NotImplementedException();
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Excluir(Comitente Entidade)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Comitente Entidade)
        {
            throw new NotImplementedException();
        }

        public Comitente SelecionarPorId(int id)
        {
            return ConsultaSQL(@"
                SELECT tb_comitentes.*, 
                       (SELECT login 
                          FROM dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios u 
                         WHERE u.id_usuario = tb_comitentes.id_usuario_cadastro) usuario_cadastro
                  FROM tb_comitentes 
                 WHERE id =" + id).Rows[0]
                 .ConverterParaEntidade<Comitente>();
        }

        public IList<Comitente> SelecionarTudo()
        {
            return ConsultaSQL(@"
                            SELECT tb_comitentes.*, u.login usuario_cadastro,
                                    (SELECT descricao FROM tb_comitentes_tipo_importacao WHERE tb_comitentes.tipo_importacao = tb_comitentes_tipo_importacao.id) descricao_tipo_importacao
                               FROM tb_comitentes
                               JOIN dbMobLinkDepositoPublicoProducao.dbo.tb_dep_usuarios u 
                                 ON tb_comitentes.id_usuario_cadastro = u.id_usuario")
                                 .ConverterParaLista<Comitente>();
        }

        public IList<Comitente> SelecionarTudo(Comitente Entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Comitente> SelecionarTudo(int id)
        {
            throw new NotImplementedException();
        }

        public IList<TabelaGenerica> SelecionarTiposImportacao()
        {
            return ConsultaSQL("SELECT * FROM dbo.tb_comitentes_tipo_importacao")
                .ConverterParaLista<TabelaGenerica>();
        }
    }
}
