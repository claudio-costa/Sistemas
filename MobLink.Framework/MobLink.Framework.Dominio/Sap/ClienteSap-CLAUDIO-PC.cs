using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobLink.Framework.Dominio.Sap
{
    public class ClienteSAP
    {
        #region MAPEAMENTO
        /*
        Campos          Descrição                   Obrigatoriedade     Tipo de dados   Tamanho
        ------------------------------------------------------------------------------------------
        IDTRANSACAO     ID da solicitação do DSIN   Sim                 INTEIRO         NA          *
        BUKRS           Código Empresa              Sim                 string          4           *   1080
        VKORG           Organização de Vendas       Sim                 string          4           *   1080
        NAME1           Nome do Cliente             Sim                 string          35          *
        STRAS           Endereço -Rua               Sim                 string          35          *
        HOUSE_NUM1      Endereço -Número            Sim                 string          10          *
        BUILDING        Edifício                    Não                 string          20
        FLOOR           Andar                       Não                 string          10
        ORT02           Bairro                      Sim                 string          35          *
        PSTLZ           Código Postal(CEP)          Sim                 string          10          *
        MCOD3           Cidade                      Sim                 string          40          *
        REGIO           Região                      Sim                 string          3           *
        TELF1           Contato -Telefone           Sim                 string          16          *
        SMTP_ADDR       Contato -E - mail           Não                 string          241
        STCD1           CNPJ                        Não                 string          16
        STCD2           CPF                         Não                 string          16
        STCD3           Inscrição Estadual          Não                 string          11
        STCD4           Inscrição Municipal         Não                 string          18
        ZWELS           Forma de Pagamento          Sim                 string          10          *
        ZTERM           Condição de Pagamento       Sim                 string          4           *   B001
        LAND1           Pais(BR)

        Modificação de parceiros apenas:
        -------------------------------------------------------------------------------------------------------------------
        TIPO_PARCEIRO   Tipo de Parceiro (F - Fornecedores / C - Clientes)	                                Sim	    string	1
        COD_PARCEIRO    Codigo do parceiro para modificação	                                                Sim	    string	10
        COD_ORG         Codigo da organização ( Compras / Vendas ) conforme o tipo de parceiro escolhido	Sim	    string	4
        */
#endregion

        public ClienteSAP()
        {
            //Codigo_Empresa = "1080";
            //Condicao_Pagamento = "B001";
            //Organizacao_Vendas = "1080";
        }

        /// <summary>
        /// IDTRANSACAO ID da solicitação do DSIN
        /// </summary>
        public string Transacao_SAP { get; set; }

        /// <summary>
        /// BUKRS           Código Empresa
        /// </summary>
        public string Codigo_Empresa { get; set; }

        /// <summary>
        /// VKORG           Organização de Vendas
        /// </summary>
        public string Organizacao_Vendas { get; set; }

        /// <summary>
        /// NAME1           Nome do Cliente
        /// </summary>
        public string Nome_Cliente { get; set; }

        /// <summary>
        /// STRAS           Endereço -Rua
        /// </summary>
        public string Endereco_Rua { get; set; }

        /// <summary>
        /// HOUSE_NUM1      Endereço -Número
        /// </summary>
        public string Endereco_Numero { get; set; }

        /// <summary>
        /// BUILDING        Edifício
        /// </summary>
        public string Endereco_Edificio { get; set; }

        /// <summary>
        /// FLOOR           Andar
        /// </summary>
        public string Endereco_Edificio_Andar { get; set; }

        /// <summary>
        /// ORT02           Bairro
        /// </summary>
        public string Endereco_Bairro { get; set; }

        /// <summary>
        /// PSTLZ           Código Postal(CEP)
        /// </summary>
        public string Endereco_CEP { get; set; }

        /// <summary>
        /// MCOD3           Cidade
        /// </summary>
        public string Endereco_Cidade { get; set; }

        /// <summary>
        /// REGIO           Região
        /// </summary>
        public string Endereco_Regiao { get; set; }

        /// <summary>
        /// TELF1           Contato -Telefone
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// SMTP_ADDR       Contato -E - mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// STCD1           CNPJ
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        /// STCD2           CPF
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// STCD3           Inscrição Estadual
        /// </summary>
        public string Inscricao_Estadual { get; set; }

        /// <summary>
        /// STCD4           Inscrição Municipal
        /// </summary>
        public string Inscricao_Municipal { get; set; }

        /// <summary>
        /// ZWELS           Forma de Pagamento
        /// </summary>
        public string Forma_Pagamento { get; set; }

        /// <summary>
        /// ZTERM           Condição de Pagamento
        /// </summary>
        public string Condicao_Pagamento { get; set; }

        /// <summary>
        /// LAND1           Pais(BR)
        /// </summary>
        public string Endereco_Pais { get; set; }


        //Modificação de parceiro

        /// <summary>
        /// TIPO_PARCEIRO   Tipo de Parceiro (F - Fornecedores / C - Clientes)
        /// </summary>
        public string Tipo_Parceiro { get; set; }

        /// <summary>
        /// COD_PARCEIRO    Codigo do parceiro para modificação
        /// </summary>
        public string Codigo_Parceiro { get; set; }

        /// <summary>
        /// COD_ORG         Codigo da organização ( Compras / Vendas )
        /// </summary>
        public string Codigo_Organizacao_Parceiro { get; set; }

        public int Id_GRV { get; set; }
    }
}
