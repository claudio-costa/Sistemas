using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPatios.DAL;

namespace WebPatios.Business
{
    public abstract class BaseBLL
    {
        //internal IList<DateTime> getSemanaCorrente()
        //{
        //    DateTime dataAtual = DateTime.Now;

        //    DateTime primeiroDiaSemana = dataAtual;
        //    DateTime ultimoDiaSemana = dataAtual;

        //    switch (dataAtual.DayOfWeek)
        //    {
        //        case DayOfWeek.Sunday:
        //            primeiroDiaSemana = dataAtual;
        //            break;
        //        case DayOfWeek.Monday:
        //            primeiroDiaSemana = dataAtual.AddDays(-1);
        //            break;
        //        case DayOfWeek.Tuesday:
        //            primeiroDiaSemana = dataAtual.AddDays(-2);
        //            break;
        //        case DayOfWeek.Wednesday:
        //            primeiroDiaSemana = dataAtual.AddDays(-3);
        //            break;
        //        case DayOfWeek.Thursday:
        //            primeiroDiaSemana = dataAtual.AddDays(-4);
        //            break;
        //        case DayOfWeek.Friday:
        //            primeiroDiaSemana = dataAtual.AddDays(-5);
        //            break;
        //        case DayOfWeek.Saturday:
        //            primeiroDiaSemana = dataAtual.AddDays(-6);
        //            break;
        //        default:
        //            break;
        //    }

        //    ultimoDiaSemana = primeiroDiaSemana.AddDays(6);

        //    var resultDatas = new List<DateTime>();

        //    resultDatas.Add(primeiroDiaSemana);
        //    resultDatas.Add(ultimoDiaSemana);

        //    //for (int i = 0; i < 6; i++)
        //    //{
        //    //    resultDatas.Add(primeiroDiaSemana.AddDays(i + 1));
        //    //}

        //    return resultDatas;
        //}

        //Método para recuperar Connection Strings
        //Método para gravar request

        //private BaseBLL.Ambiente getAmbiente()
        //{
        //    var ambiente = System.Configuration.ConfigurationManager.AppSettings["AMBIENTE"].ToString();
        //    return (BaseBLL.Ambiente)Enum.Parse(typeof(BaseBLL.Ambiente), ambiente, true);
        //}

        internal int _idDeposito { get; set; }
        internal int _idCliente { get; set; }

        internal BaseBLL()
        {

        }

        internal enum Ambiente
        {
            Desenvolvimento = 0, QA = 1, Produção = 2
        }

        internal BaseBLL(Ambiente Ambiente)
        {
            ConnectionFactory.connectionString = getConnectionString(Ambiente);
        }
        
        private string getConnectionString(Ambiente Ambiente)
        {
            string res = string.Empty;

            switch (Ambiente)
            {
                case Ambiente.Desenvolvimento:
                    res = @"Server = 179.107.47.90; Database = dbMobLinkSAP; User Id = sapdev; Password = Buracica#;";
                    break;
                case Ambiente.QA:
                    res = @"Server = 179.107.47.91; Database = dbMobLinkSAP; User Id = sapdev; Password = Buracica#;";
                    break;
                case Ambiente.Produção:
                    res = @"";
                    break;
                default:
                    res = @"";
                    break;
            }

            return res;
        }


        internal int executaSQL(string SQL)
        {
            return ConnectionFactory.Executar(SQL);
        }

        internal object executaSQLEscalar(string SQL)
        {
            return ConnectionFactory.ExecutarEscalar(SQL);
        }

        internal System.Data.DataTable ConsultaSQL(string SQL)
        {
            return ConnectionFactory.Consulta(SQL);
        }

        //internal T ConversaoSegura<T>(object valor, T valorPadrao)
        //{
        //    try
        //    {
        //        return valor == null ? default(T) :
        //            (T)Convert.ChangeType(valor, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));
        //    }
        //    catch
        //    {
        //        return valorPadrao;
        //    }
        //}
    }
}
