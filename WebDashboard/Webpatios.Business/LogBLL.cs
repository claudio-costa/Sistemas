﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPatios.Business
{
    public class LogBLL : BaseBLL
    {
        public LogBLL()
        {

        }

        public void InserirLogAcesso(Model.Usuario usuario)
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"

            INSERT INTO tb_log_webpatios_acesso
                       (id_usuario, ip_usuario)
                 VALUES ({0}, '{1}')", usuario.idUsuario, usuario.ipUsuario);
            #endregion

            try
            {
                executaSQL(SQL.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<Model.Log.LogAcesso> ConsultaLogAcesso()
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"SELECT id, data_hora_visita, id_usuario FROM tb_log_webpatios_acesso");
            #endregion

            var logs = new List<Model.Log.LogAcesso>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    logs.Add(new Model.Log.LogAcesso()
                    {
                        dataHoraAcesso = CCFW.Conversoes.ConversaoSegura(tbRes.Rows[0]["data_hora_visita"].ToString(), DateTime.Now),
                        idUsuario = CCFW.Conversoes.ConversaoSegura(tbRes.Rows[0]["id_usuario"].ToString(), -1)
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return logs;
        }
    }
}
