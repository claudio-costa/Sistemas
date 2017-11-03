using MobLink.LinkLeiloes.Repositorio;
using MobLink.LinkLeiloes.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MobLink.LinkLeiloes.Web.Security
{
    public class PermissaoProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (Constantes.UsuarioLogado == null)
            {
                FormsAuthentication.SignOut();
                new UsuarioController().Login();
                return new string[] { };
            }
            
            List<string> permissoes = Constantes.UsuarioLogado.Modulos.Select(p => p.Formulario).ToList();

            List<string> menus = Constantes.UsuarioLogado.Modulos.Select(p => p.Menu).ToList();

            List<string> submenus = Constantes.UsuarioLogado.Modulos.Select(p => p.SubMenu).ToList();

            return (permissoes.Union(menus).Union(submenus)).ToArray();
        }
        
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}