using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MobLink.LinkLeiloes.Web
{
    public class MSOffice
    {
        public class MSWord
        {
            public void GerarArquivoWord<T>(List<T> Dados)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/msword";
                
                var strNomeArquivo = "DocumentoGerado1" + ".doc";
                
                HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + strNomeArquivo);

                var strHTMLContent = new StringBuilder();
                strHTMLContent.Append("<h1 title='Heading' align='Center' style='font-family: verdana; font -size: 80 % ; color: black'><u> Documento - Word - tabela </u> </h1> ").ToString();
                strHTMLContent.Append("<br>".ToString());
                strHTMLContent.Append("<table align='Center'>".ToString());
                strHTMLContent.Append("<tr>".ToString());
                strHTMLContent.Append("<td style='width:100px; background:# 99CCE0'><b>Nome </b> </td>".ToString());
                strHTMLContent.Append("<td style='width:100px; background:# 99CCE0'><b>Cidade </b> </td>".ToString());
                strHTMLContent.Append("<td style='width:100px; background:# 99CCE0'><b>Pais </b> </td>".ToString());
                strHTMLContent.Append("<td style='width:100px; background:# 99CCE0'><b>Fone </b> </td>".ToString());
                strHTMLContent.Append("</tr>".ToString());

                //primeira linha de dados - a tabela
                strHTMLContent.Append("<tr>".ToString());
                //strHTMLContent.Append("<td style='width:100px'>" & cliente.ContactName & " </td>".ToString());
                //strHTMLContent.Append("<td style='width:100px'>" & cliente.City & " </td>".ToString());
                //strHTMLContent.Append("<td style='width:100px'>" & cliente.Country & "</td>".ToString());
                //strHTMLContent.Append("<td style='width:100px'>" & cliente.Phone & "</td>".ToString());
                strHTMLContent.Append("</tr>".ToString());
                strHTMLContent.Append("</table>".ToString());
                strHTMLContent.Append("<br><br>".ToString());

                //segunda linha de dados: o texto

                //strHTMLContent.Append("<b> " & cliente.ContactName & "</b>" & " vem por meio desta declarar que ".ToString());
                //strHTMLContent.Append(" reside em  <b>" & cliente.City & "</b> -  <b>" & cliente.Country & "</b>".ToString());
                //strHTMLContent.Append(" telefone : <b>" & cliente.Phone & "</b>".ToString());
                strHTMLContent.Append("<br><br>".ToString());
                strHTMLContent.Append("</table>".ToString());
                strHTMLContent.Append("<br><br>".ToString());
                strHTMLContent.Append("<p align='Center'> Documento Word gerado dinamicamente </p> ".ToString());
                HttpContext.Current.Response.Write(strHTMLContent);
                //HttpContext.Current.Response.[End]();
                HttpContext.Current.Response.Flush();
            }
        }
    }
}