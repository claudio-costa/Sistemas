using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WSSap.Repositorio
{
    public class BaseRepositorio : Framework.Database.DbSqlServer
    {
        public BaseRepositorio(string ConnectionString) : base(ConnectionString)
        {

        }
    }
}
