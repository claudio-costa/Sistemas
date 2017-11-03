using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPatios.Business
{
    public class EntradaBLL : BaseBLL
    {
        public EntradaBLL(Model.Deposito filtroDeposito)
        {
            base._idCliente = filtroDeposito.clienteDeposito.idCliente;
            base._idDeposito = filtroDeposito.idDeposito;
        }

        public EntradaBLL()
        {

        }

        public IList<Model.Entradas.EntradaTeste> getEntradasGrafico()
        {
            var entradas = new List<Model.Entradas.EntradaTeste>();

            entradas.Add(new Model.Entradas.EntradaTeste() { descricaoDivisao = "policia", quantidade = 45 });
            entradas.Add(new Model.Entradas.EntradaTeste() { descricaoDivisao = "prefeitura", quantidade = 62 });
            entradas.Add(new Model.Entradas.EntradaTeste() { descricaoDivisao = "cliente 1", quantidade = 34 });
            entradas.Add(new Model.Entradas.EntradaTeste() { descricaoDivisao = "PM", quantidade = 71 });
            entradas.Add(new Model.Entradas.EntradaTeste() { descricaoDivisao = "Detran", quantidade = 48 });

            return entradas;
        }

        public IList<Model.Entradas.EntradaAnalitica> getEntradasAnalitico(string periodo)
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"   
                SELECT id_grv, numero_formulario_grv, nome_autoridade_responsavel, placa, renavam, data_hora_guarda, divisao
                  FROM tb_dep_grv grv
                  LEFT JOIN tb_dep_autoridades_responsaveis aut ON grv.id_autoridade_responsavel = aut.id_autoridade_responsavel
                 WHERE id_cliente = {0} AND id_deposito = {1}", _idCliente, _idDeposito);
            #endregion

            var entradasAnaliticas = new List<Model.Entradas.EntradaAnalitica>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow linha in tbRes.Rows)
                    {
                        var itemEntrada = new Model.Entradas.EntradaAnalitica();
                        itemEntrada.data_hora_guarda = linha["data_hora_guarda"].ToString();
                        itemEntrada.divisao = linha["divisao"].ToString();
                        itemEntrada.id_grv = linha["id_grv"].ToString();
                        itemEntrada.nome_autoridade_responsavel = linha["nome_autoridade_responsavel"].ToString();
                        itemEntrada.numero_formulario_grv = linha["numero_formulario_grv"].ToString();
                        itemEntrada.placa = linha["placa"].ToString();
                        itemEntrada.renavam = linha["renavam"].ToString();

                        entradasAnaliticas.Add(itemEntrada);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return entradasAnaliticas;
        }

        public IList<Model.Entradas.Entrada> getEntradas(string periodo)
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"   
               SELECT ISNULL(aut.divisao,'OUTROS') as divisao, COUNT(grv.id_grv) AS qtd
                 FROM tb_dep_grv grv 
                 LEFT JOIN tb_dep_autoridades_responsaveis aut ON grv.id_autoridade_responsavel = aut.id_autoridade_responsavel
                WHERE id_cliente = {0} AND id_deposito = {1}
                GROUP BY grv.id_autoridade_responsavel, aut.divisao
                ORDER BY qtd DESC", _idCliente, _idDeposito);

            #endregion

            var entradas = new List<Model.Entradas.Entrada>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow linha in tbRes.Rows)
                    {
                        var itemEntrada = new Model.Entradas.Entrada();
                        itemEntrada.descricaoDivisao = linha["divisao"].ToString();
                        itemEntrada.quantidade = int.Parse(linha["qtd"].ToString());
                        entradas.Add(itemEntrada);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return entradas;
        }

        public IList<Model.Entradas.EntradaTiposVeiculo> getEntradasTipoVeiculo()
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"   

            SELECT dbo.tb_dep_tipo_veiculos.descricao tipoveiculo,
                   COUNT(dbo.tb_dep_grv.id_grv) AS QteRecolhidos        	
              FROM dbo.tb_dep_grv 
             INNER JOIN dbo.tb_dep_tarifas_tipo_veiculos ON dbo.tb_dep_grv.id_tarifa_tipo_veiculo = dbo.tb_dep_tarifas_tipo_veiculos.id_tarifa_tipo_veiculo 
             INNER JOIN dbo.tb_dep_tipo_veiculos ON dbo.tb_dep_tarifas_tipo_veiculos.id_tipo_veiculo = dbo.tb_dep_tipo_veiculos.id_tipo_veiculo
             WHERE id_cliente = {0}
               AND id_deposito = {1}
             GROUP BY dbo.tb_dep_tipo_veiculos.descricao
             ORDER BY QteRecolhidos DESC", _idCliente, _idDeposito);

            #endregion

            var entradas = new List<Model.Entradas.EntradaTiposVeiculo>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow linha in tbRes.Rows)
                    {
                        var itemEntrada = new Model.Entradas.EntradaTiposVeiculo();
                        itemEntrada.tipoVeiculo = linha["tipoveiculo"].ToString();
                        itemEntrada.quantidade = int.Parse(linha["QteRecolhidos"].ToString());
                        entradas.Add(itemEntrada);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return entradas;
        }

        public IList<Model.Entradas.EntradaDiaSemana> getEntradasDiaSemana()
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"
                         SELECT CASE DATEPART(DW,data_hora_remocao) 
			                    WHEN 1 THEN 'DOMINGO' 
			                    WHEN 2 THEN 'SEGUNDA-FEIRA' 
			                    WHEN 3 THEN 'TERÇA-FEIRA' 
			                    WHEN 4 THEN 'QUARTA-FEIRA' 
			                    WHEN 5 THEN 'QUINTA-FEIRA' 
			                    WHEN 6 THEN 'SEXTA-FEIRA' 
			                    WHEN 7 THEN 'SÁBADO' 
			                    END AS DiaSemana,
			                    COUNT(id_grv) AS QteRecolhidos 
                           FROM tb_dep_grv
                          WHERE id_cliente = {0} AND id_deposito = {1}
                       GROUP BY DATEPART(DW,data_hora_remocao)", _idCliente, _idDeposito);
            #endregion

            var entradas = new List<Model.Entradas.EntradaDiaSemana>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow linha in tbRes.Rows)
                    {
                        var item = new Model.Entradas.EntradaDiaSemana();
                        item.diaSemana = linha["DiaSemana"].ToString();
                        item.quantidade = int.Parse(linha["QteRecolhidos"].ToString());
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

        public IList<Model.Entradas.EntradaHora> getEntradasHora()
        {
            #region CONSULTA
            string SQL;
            SQL = string.Format(@"
                         SELECT DATEPART(HH,data_hora_remocao) AS HoraDia,
	                            COUNT(id_grv) AS QteRecolhidos 
                        FROM tb_dep_grv
                        WHERE id_cliente = {0} AND id_deposito = {1}
                    GROUP BY DATEPART(HH,data_hora_remocao)", _idCliente, _idDeposito);
            #endregion

            var entradas = new List<Model.Entradas.EntradaHora>();

            try
            {
                var tbRes = ConsultaSQL(SQL.ToString());

                if (tbRes.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow linha in tbRes.Rows)
                    {
                        var item = new Model.Entradas.EntradaHora();
                        item.hora = linha["HoraDia"].ToString();
                        item.quantidade = int.Parse(linha["QteRecolhidos"].ToString());
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
