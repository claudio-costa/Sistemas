using System.Collections.Generic;
using System.Data;

namespace MobLink.Framework.Database
{
    public abstract class Db
    {
        protected string ConnectionString { get; set; }

        protected abstract DataTable ConsultaSQL(string sql);

        protected abstract List<T> ConsultaSQL<T>(string sql) where T : class;

        protected abstract int ExecutaSQL(string sql);

        protected abstract int ExecutaSQL_ScopeIdentity(string sql);
    }
}
