using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCFW;

namespace WebPatios.Business
{
    public class UsuarioBLL : BaseBLL
    {
        public Model.Usuario login(string usuario, string senha, string IP)
        {
            #region CONSULTA
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("   SELECT clientes.id_cliente, clientes.nome,");
            SQL.AppendLine("          usuariodeposito.id_deposito , usuariodeposito.descricao,");
            SQL.AppendLine("          usuario.email, usuario.senha1, usuario.login, usuario.id_usuario,");
            SQL.AppendLine("          clientes.nome + ' - PÁTIO ' + usuariodeposito.descricao as clientedeposito");
            SQL.AppendLine("     FROM tb_dep_usuarios usuario");
            SQL.AppendLine("    INNER JOIN vw_dep_usuarios_depositos usuariodeposito on usuario.id_usuario = usuariodeposito.id_usuario");
            SQL.AppendLine("    INNER JOIN tb_dep_clientes_depositos clientedeposito on usuariodeposito.id_deposito = clientedeposito.id_deposito");
            SQL.AppendLine("    INNER JOIN tb_dep_clientes clientes on clientedeposito.id_cliente = clientes.id_cliente");
            SQL.AppendFormat("  WHERE usuario.login = '{0}'", usuario);
            SQL.AppendFormat("    AND usuario.senha1 = HASHBYTES('MD5', '{0}')", senha.ToUpper());
            SQL.AppendLine("      AND usuariodeposito.flag_ativo = 'S'");
            SQL.AppendLine("      AND usuario.flag_ativo = 'S'");
            SQL.AppendLine(" ORDER BY clientes.id_cliente");

            #endregion

            var usuarioLogado = new Model.Usuario();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    usuarioLogado.idUsuario = Conversoes.ConversaoSegura(tbRes.Rows[0]["id_usuario"], -1);
                    usuarioLogado.loginUsuario = Conversoes.ConversaoSegura(tbRes.Rows[0]["login"], string.Empty);
                    usuarioLogado.emailUsuario = Conversoes.ConversaoSegura(tbRes.Rows[0]["email"], string.Empty);
                    usuarioLogado.ipUsuario = IP;

                    foreach (System.Data.DataRow dep in tbRes.Rows)
                    {
                        var Deposito = new Model.Deposito();

                        Deposito.idDeposito = Conversoes.ConversaoSegura(dep["id_deposito"], 0);
                        Deposito.descricaoDeposito = Conversoes.ConversaoSegura(dep["descricao"], string.Empty);
                        Deposito.descricaoClienteDeposito = Conversoes.ConversaoSegura(dep["clientedeposito"], string.Empty);

                        Deposito.clienteDeposito.idCliente = Conversoes.ConversaoSegura(dep["id_cliente"], -1);
                        Deposito.clienteDeposito.nomeCliente = Conversoes.ConversaoSegura(dep["nome"], string.Empty);

                        usuarioLogado.depositosUsuario.Add(Deposito);

                        var Cliente = new Model.Cliente();

                        //Cliente.idCliente = ConversaoSegura(tbRes.Rows[0]["id_cliente"], -1);
                        Cliente.idCliente = Conversoes.ConversaoSegura(dep["id_cliente"], -1);
                        Cliente.nomeCliente = Conversoes.ConversaoSegura(dep["nome"], string.Empty);
                        usuarioLogado.clientesUsuario.Add(Cliente);
                    }
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }

            return usuarioLogado;
        }
    }
}
