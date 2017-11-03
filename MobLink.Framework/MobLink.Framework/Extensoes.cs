using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MobLink.Framework
{
    /// <summary>
    /// Classe de extensões
    /// </summary>
    public static class Extensoes
    {
        public static T ConverterParaEntidade<T>(this DataRow Linha) where T : new()
        {
            if (Linha == null) return new T();

            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();

            foreach (DataColumn col in Linha.Table.Columns)
            {
                string colName = col.ColumnName;

                // Look for the object's property with the columns name, ignore case
                PropertyInfo pInfo = t.GetProperty(colName.ToLower(),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // did we find the property ?
                if (pInfo != null)
                {
                    object val = Linha[colName];

                    // is this a Nullable<> type
                    bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                    if (IsNullable)
                    {
                        if (val is System.DBNull)
                        {
                            val = null;
                        }
                        else
                        {
                            // Convert the db type into the T we have in our Nullable<T> type
                            val = Convert.ChangeType(val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                        }
                    }
                    else
                    {
                        // Convert the db type into the type of the property in our entity
                        val = Convert.ChangeType(val, pInfo.PropertyType);
                    }
                    // Set the value of the property with the value from the db
                    pInfo.SetValue(returnObject, val, null);
                }
            }

            // return the entity object with values
            return returnObject;
        }

        public static T ConverterParaEntidade<T>(this DataTable Tabela, int Linha = 0) where T : new()
        {
            if (Tabela == null) return new T();

            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();

            foreach (DataColumn col in Tabela.Columns)
            {
                string coluna = col.ColumnName;

                // Look for the object's property with the columns name, ignore case
                PropertyInfo pInfo = t.GetProperty(coluna.ToLower(),
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                // did we find the property ?
                if (pInfo != null)
                {
                    object val = Tabela.Rows[Linha][coluna];

                    // is this a Nullable<> type
                    bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                    if (IsNullable)
                    {
                        if (val is System.DBNull)
                        {
                            val = null;
                        }
                        else
                        {
                            // Convert the db type into the T we have in our Nullable<T> type
                            val = Convert.ChangeType(val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                        }
                    }
                    else
                    {
                        // Convert the db type into the type of the property in our entity
                        val = Convert.ChangeType(val, pInfo.PropertyType);
                    }
                    // Set the value of the property with the value from the db
                    pInfo.SetValue(returnObject, val, null);
                }
            }

            // return the entity object with values
            return returnObject;
        }

        public static List<T> ConverterParaLista<T>(this DataTable Tabela) where T : new()
        {
            if (Tabela == null) return new List<T>();

            Type t = typeof(T);

            // Create a list of the entities we want to return
            List<T> returnObject = new List<T>();

            // Iterate through the DataTable's rows
            foreach (DataRow dr in Tabela.Rows)
            {
                // Convert each row into an entity object and add to the list
                T newRow = dr.ConverterParaEntidade<T>();
                returnObject.Add(newRow);
            }

            // Return the finished list
            return returnObject;
        }
        
        public static Int32 ToInt32(this object cadeia)
        {
            try
            {
                return Convert.ToInt32(cadeia);
            }
            catch
            {
                return 0;
            }
        }

        public static string DadoUnico(this DataTable tabela)
        {
            try
            {
                return tabela.Rows[0][0].ToString();
            }
            catch
            {
                return "";
            }
        }

        //public static DataTable ConverterParaDataTable(this object Objeto)
        //{
        //    // Retrieve the entities property info of all the properties
        //    PropertyInfo[] pInfos = Objeto.GetType().GetProperties();

        //    // Create the new DataTable
        //    var table = new DataTable();

        //    // Iterate on all the entities' properties
        //    foreach (PropertyInfo pInfo in pInfos)
        //    {
        //        // Create a column in the DataTable for the property
        //        table.Columns.Add(pInfo.Name, pInfo.GetType());
        //    }

        //    // Create a new row of values for this entity
        //    DataRow row = table.NewRow();
        //    // Iterate again on all the entities' properties
        //    foreach (PropertyInfo pInfo in pInfos)
        //    {
        //        // Copy the entities' property value into the DataRow
        //        row[pInfo.Name] = pInfo.GetValue(Objeto, null);
        //    }

        //    // Return the finished DataTable
        //    return table;
        //}

        public static string SomentePrimeiraPalavra(this string Texto)
        {
            if (!string.IsNullOrEmpty(Texto))
                return Texto.Split(' ')[0];
            else
                return "";
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static string FormatarParaCEP(this string s)
        {
            if (s.Length != 8) return "ERRO";



            return s.Substring(0, 5) + "-" + s.Substring(5, 3);
        }


    }
}
