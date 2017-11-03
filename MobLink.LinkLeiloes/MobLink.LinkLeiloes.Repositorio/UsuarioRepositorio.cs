using MobLink.Framework;
using MobLink.Framework.Database;
using MobLink.LinkLeiloes.Dominio;
using System.Data;
using System.Linq;


namespace MobLink.LinkLeiloes.Repositorio
{
    public class UsuarioRepositorio : DbSqlServer
    {
        protected internal UsuarioRepositorio()
            : base(Util.LerConfiguracao("CONEXAO_DP"))
        {
            
        }

        private Usuario GetUsuario(string Login, string Senha)
        {
            string sql = string.Format(@"
            SELECT u.id_usuario id, u.login, u.senha1 Senha, u.nome, u.nome_completo NomeCompleto, u.data_cadastro datacadastro, *
                FROM vw_dep_usuarios_nome u
                WHERE login = '{0}'
                --AND senha1 = hashbytes('MD5', upper('{1}'))
                AND u.flag_ativo = 'S'", Login.ToUpper().Trim(), Senha.ToUpper().Trim());
            
            var dtUsuario = ConsultaSQL(sql);

            if (dtUsuario.Rows.Count == 0) return new Usuario();

            var usuario = dtUsuario
                .Rows[0]
                .ConverterParaEntidade<Usuario>();

            sql = string.Format(@"
            SELECT pau.id_perfil_acesso, pau.id_usuario, pa.descricao, pa.data_cadastro, pa.flag_ativo
              FROM dbo.tb_dep_sistema_perfil_acesso_usuarios pau
              JOIN dbo.tb_dep_sistema_perfil_acesso pa ON pau.id_perfil_acesso = pa.id_perfil_acesso
             WHERE pau.id_usuario = {0}
               AND pa.flag_ativo = 'S'", usuario.Id);

            var dtPerfis = ConsultaSQL(sql);
            usuario.PerfisAcesso = dtPerfis.ConverterParaLista<PerfilAcesso>();

            sql = string.Format(@"
            SELECT SSM.id_modulo, PASM.id_sub_modulo, 
                   UPPER(SM.descricao) DESCRICAOMODULO, UPPER(SM.menu) MENU, UPPER(SSM.menu) SUBMENU, 
                   UPPER(ssm.formulario) FORMULARIO, UPPER(SSM.descricao) DESCRICAO
             FROM tb_dep_sistema_perfil_acesso_sub_modulos PASM
             JOIN tb_dep_sistema_sub_modulos SSM ON SSM.id_sub_modulo = PASM.id_sub_modulo
             JOIN tb_dep_sistema_modulos SM ON SM.id_modulo = SSM.id_modulo
            WHERE id_perfil_acesso IN ({0})
              AND SSM.status = 'S'", string.Join(",", usuario.PerfisAcesso.Select(p => p.Id_Perfil_Acesso)));

            var dtModulos = ConsultaSQL(sql);
            usuario.Modulos = dtModulos.ConverterParaLista<Modulo>();

            return usuario;
        }

        public Usuario AutenticarUsuario(string Login, string Senha)
        {
            Usuario user = GetUsuario(Login, Senha);

            return user;

            //StringBuilder sql = new StringBuilder();

            //sql.AppendLine("SELECT id_usuario, login, senha1, flag_ativo");

            //sql.AppendLine("FROM dbo.tb_dep_usuarios");

            //sql.AppendLine(string.Format("WHERE login = '{0}'", Login.Trim().ToUpper()));

            //sql.AppendLine(string.Format("AND senha1 = hashbytes('MD5', '{0}')", Senha.Trim().ToUpper()));

            //var usuario = ConsultaSQL(sql.ToString()).AsEnumerable().FirstOrDefault();

            //if (usuario != null)
            //{
            //    return usuario.ConverterParaEntidade<Usuario>().Login == Login.ToUpper().Trim();
            //}
            //else
            //{
            //    return false;
            //}
        }

        public Usuario SelecionarUsuario(int id)
        {
            string sql = string.Format("SELECT* FROM dbMobLinkDepositoPublicoProducao.dbo.vw_dep_usuarios_nome WHERE id_usuario = {0}", id);

            var user = ConsultaSQL(sql).Rows[0].ConverterParaEntidade<Usuario>();

            return user;
        }
    }
}
