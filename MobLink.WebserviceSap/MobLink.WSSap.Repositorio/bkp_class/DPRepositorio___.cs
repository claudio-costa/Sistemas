using MobLink.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.WSSap.Repositorio
{
    public class DPRepositorio___ : Framework.Database.DbSqlServer
    {
        public DPRepositorio___() : base(Framework.Util.LerConfiguracao("CONEXAO_DP"))
        {

        }

        internal string CapturaIndicadorAgrupamento(string codigoMaterial)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT flag_agrupamento FROM dbo.tb_dep_sap_tipo_composicao WHERE codigo_material = '{0}'", codigoMaterial);
            
            return ConsultaSQL(sql.ToString()).DadoUnico();
        }

        internal static string CapturaGrupo(string codigoMaterial)
        {
            DPRepositorio___ rep = new DPRepositorio___();

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"SELECT id_sap_tipo_composicao_grupos
                                 FROM dbo.tb_dep_sap_tipo_composicao
                                WHERE codigo_material = '{0}'
                                  AND flag_agrupamento = 'S'", codigoMaterial);

            return rep.ConsultaSQL(sql.ToString()).DadoUnico();
        }

        internal static string CapturaMaterialAgrupamento(int id_grupo)
        {
            DPRepositorio___ rep = new DPRepositorio___();

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"SELECT codigo_material 
                                 FROM tb_dep_sap_tipo_composicao
                                WHERE id_sap_tipo_composicao = (SELECT id_sap_tipo_composicao_material_agrupamento 
                                                                  FROM tb_dep_sap_tipo_composicao_grupos
                                                                 WHERE id_sap_tipo_composicao_grupos = {0})", id_grupo);

            return rep.ConsultaSQL(sql.ToString()).DadoUnico();
        }

        internal static List<GrupoAgrupamento> SelecionaGrupos()
        {
            DPRepositorio___ rep = new DPRepositorio___();

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"SELECT * FROM tb_dep_sap_tipo_composicao_grupos");

            return rep.ConsultaSQL(sql.ToString()).ConverterParaLista<GrupoAgrupamento>();
        }

        internal static string CapturaTipoDocumentoAgrupamento(int id_grupo)
        {
            DPRepositorio___ rep = new DPRepositorio___();

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"SELECT tipo_documento_venda 
                                 FROM tb_dep_sap_tipo_composicao
                                WHERE id_sap_tipo_composicao = (SELECT id_sap_tipo_composicao_material_agrupamento 
                                                                  FROM tb_dep_sap_tipo_composicao_grupos
                                                                 WHERE id_sap_tipo_composicao_grupos = {0})", id_grupo);

            return rep.ConsultaSQL(sql.ToString()).DadoUnico();
        }
    }
}
