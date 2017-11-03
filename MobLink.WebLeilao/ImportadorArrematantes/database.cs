using MobLink.Framework;


namespace ImportadorArrematantes
{
    public class database : Sql
    {
        protected internal database(): base(Sistemas.Leilao, DetectarAmbiente())
        {
            
        }

        public System.Data.DataTable ConsultaTabelas(string sql)
        {
            return ConsultaSQL(sql);
        }

        public static database Arrematante
        {
           get { return new database(); }
        }
    }
}
