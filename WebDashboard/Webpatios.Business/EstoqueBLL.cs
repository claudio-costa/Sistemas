﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPatios.Business
{
    public class EstoqueBLL : BaseBLL
    {
        public EstoqueBLL(Model.Deposito filtroDeposito)
        {
            base._idCliente = filtroDeposito.clienteDeposito.idCliente;
            base._idDeposito = filtroDeposito.idDeposito;
        }

        public EstoqueBLL()
        {

        }

        public IList<Model.Estoque.EstoqueTipoVeiculo> getEstoqueTipoVeiculo()
        {
            #region CONSULTA
            string SQL;
            //SQL = string.Format(@"   SELECT dbo.tb_dep_tipo_veiculos.descricao AS TipoVeiculo,
            //                       dbo.tb_dep_grv.id_cliente,
            //                       dbo.tb_dep_grv.id_deposito,
            //                       COUNT(dbo.tb_dep_grv.id_grv) AS Estoque
            //                           FROM dbo.tb_dep_grv 
            //            INNER JOIN dbo.tb_dep_tarifas_tipo_veiculos 
            //                             ON dbo.tb_dep_grv.id_tarifa_tipo_veiculo = dbo.tb_dep_tarifas_tipo_veiculos.id_tarifa_tipo_veiculo 
            //            INNER JOIN dbo.tb_dep_tipo_veiculos 
            //                             ON dbo.tb_dep_tarifas_tipo_veiculos.id_tipo_veiculo = dbo.tb_dep_tipo_veiculos.id_tipo_veiculo
            //                          WHERE id_status_operacao IN ('1','2','B','G','L','M','P','T' )
            //                            AND id_cliente = {0} and id_deposito = {1}
            //                       GROUP BY dbo.tb_dep_tipo_veiculos.descricao, dbo.tb_dep_grv.id_cliente, dbo.tb_dep_grv.id_deposito
            //                       ORDER BY Estoque DESC", _idCliente, _idDeposito);

            SQL = string.Format(@"   SELECT dbo.tb_dep_tipo_veiculos.descricao AS TipoVeiculo,
                                    dbo.tb_dep_grv.id_cliente,
                                    dbo.tb_dep_grv.id_deposito,
                                    COUNT(dbo.tb_dep_grv.id_grv) AS Estoque
                               FROM dbo.tb_dep_grv
                         INNER JOIN dbo.tb_dep_tipo_veiculos ON tb_dep_grv.id_tipo_veiculo = tb_dep_tipo_veiculos.id_tipo_veiculo
                              WHERE id_status_operacao IN('1', '2', 'B', 'G', 'L', 'M', 'P', 'T')
                                AND id_cliente = {0} and id_deposito = {1}
                           GROUP BY dbo.tb_dep_tipo_veiculos.descricao, dbo.tb_dep_grv.id_cliente, dbo.tb_dep_grv.id_deposito
                           ORDER BY Estoque DESC", _idCliente, _idDeposito);

            

            #endregion

            var entradas = new List<Model.Estoque.EstoqueTipoVeiculo>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow linha in tbRes.Rows)
                    {
                        var item = new Model.Estoque.EstoqueTipoVeiculo();
                        item.tipoVeiculo = linha["TipoVeiculo"].ToString();
                        item.quantidade = int.Parse(linha["Estoque"].ToString());
                        entradas.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return entradas;
        }
    }
}
