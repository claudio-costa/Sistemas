using MobLink.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WSSap.Repositorio
{
    public class DepositoPublicoRepositorio : BaseRepositorio
    {
        public DepositoPublicoRepositorio() : base(Framework.Util.LerConfiguracao("CONEXAO_DP"))
        {

        }

        internal string CapturaIndicadorAgrupamento(string codigo_material)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT flag_agrupamento FROM dbo.tb_dep_sap_tipo_composicao WHERE codigo_material = '{0}'", codigo_material);

            return ConsultaSQL(sql.ToString()).DadoUnico();
        }

        internal string CapturaGrupo(string codigo_material)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"SELECT id_sap_tipo_composicao_grupos
                                 FROM dbo.tb_dep_sap_tipo_composicao
                                WHERE codigo_material = '{0}'", codigo_material);

            return ConsultaSQL(sql.ToString()).DadoUnico();
        }

        internal string CapturaMaterialAgrupamento(int id_grupo)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"SELECT codigo_material 
                                 FROM tb_dep_sap_tipo_composicao
                                WHERE id_sap_tipo_composicao = (SELECT id_sap_tipo_composicao_material_agrupamento 
                                                                  FROM tb_dep_sap_tipo_composicao_grupos
                                                                 WHERE id_sap_tipo_composicao_grupos = {0})", id_grupo);

            return ConsultaSQL(sql.ToString()).DadoUnico();
        }

        internal List<Dominio.tb_dep_sap_tipo_composicao_grupos> SelecionaGrupos()
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"SELECT * FROM tb_dep_sap_tipo_composicao_grupos");

            return ConsultaSQL(sql.ToString()).ConverterParaLista<Dominio.tb_dep_sap_tipo_composicao_grupos>();
        }

        internal string CapturaTipoDocumentoAgrupamento(int id_grupo)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"SELECT tipo_documento_venda 
                                 FROM tb_dep_sap_tipo_composicao
                                WHERE id_sap_tipo_composicao = (SELECT id_sap_tipo_composicao_material_agrupamento 
                                                                  FROM tb_dep_sap_tipo_composicao_grupos
                                                                 WHERE id_sap_tipo_composicao_grupos = {0})", id_grupo);

            return ConsultaSQL(sql.ToString()).DadoUnico();
        }
    }
}
