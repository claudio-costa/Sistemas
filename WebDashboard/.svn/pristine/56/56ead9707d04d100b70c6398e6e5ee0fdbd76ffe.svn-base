using System;
using System.Collections.Generic;
using WebPatios.Model.Saidas;

namespace WebPatios.Business
{
    public class SaidaBLL : BaseBLL
    {
        public SaidaBLL(Model.Deposito filtroDeposito)
        {
            base._idCliente = filtroDeposito.clienteDeposito.idCliente;
            base._idDeposito = filtroDeposito.idDeposito;
        }

        public SaidaBLL()
        {

        }

        public List<Saida> getSaidas()
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"   
                SELECT ISNULL(divisao,'OUTROS') as divisao, COUNT(*) AS qtd
                  FROM vw_dep_relatorio_liberacao
                WHERE id_cliente = {0} AND id_deposito = {1}
                GROUP BY divisao
                ORDER BY qtd DESC", _idCliente, _idDeposito);
            #endregion

            var saidas = new List<Model.Saidas.Saida>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow linha in tbRes.Rows)
                    {
                        var itemEntrada = new Model.Saidas.Saida();
                        itemEntrada.descricaoDivisao = linha["divisao"].ToString();
                        itemEntrada.quantidade = int.Parse(linha["qtd"].ToString());
                        saidas.Add(itemEntrada);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return saidas;
        }

        public IList<SaidaTiposVeiculo> getSaidasTipoVeiculo()
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"   

            SELECT ISNULL(tipo_veiculo,'OUTROS') as TIPO, COUNT(*) AS QTDE
                  FROM vw_dep_relatorio_liberacao
                WHERE id_cliente = {0} AND id_deposito = {1}
                GROUP BY tipo_veiculo
                ORDER BY QTDE DESC", _idCliente, _idDeposito);

            #endregion

            var saidas = new List<SaidaTiposVeiculo>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow linha in tbRes.Rows)
                    {
                        var item = new SaidaTiposVeiculo();
                        item.tipoVeiculo = linha["TIPO"].ToString();
                        item.quantidade = int.Parse(linha["QTDE"].ToString());
                        saidas.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return saidas;
        }

        public IList<SaidaDiaSemana> getSaidasDiaSemana()
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"
            
                SELECT CASE DATEPART(DW, data_liberacao) 
			                    WHEN 1 THEN 'DOMINGO' 
			                    WHEN 2 THEN 'SEGUNDA-FEIRA' 
			                    WHEN 3 THEN 'TERÇA-FEIRA' 
			                    WHEN 4 THEN 'QUARTA-FEIRA' 
			                    WHEN 5 THEN 'QUINTA-FEIRA' 
			                    WHEN 6 THEN 'SEXTA-FEIRA' 
			                    WHEN 7 THEN 'SÁBADO' 
			                    END AS DIA,
			                    COUNT(*) AS QTDE
                  FROM vw_dep_relatorio_liberacao
                  WHERE id_cliente = {0} AND id_deposito = {1}
                GROUP BY DATEPART(DW, data_liberacao)", _idCliente, _idDeposito);
            #endregion

            var saidas = new List<SaidaDiaSemana>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow linha in tbRes.Rows)
                    {
                        var item = new SaidaDiaSemana();
                        item.diaSemana = linha["DIA"].ToString();
                        item.quantidade = int.Parse(linha["QTDE"].ToString());
                        saidas.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return saidas;
        }
    }
}
