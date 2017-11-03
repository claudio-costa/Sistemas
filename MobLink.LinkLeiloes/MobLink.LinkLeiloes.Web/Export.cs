using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MobLink.LinkLeiloes.Web
{
    public class Export
    {
        public void ToExcel_XLSX<T>(HttpResponseBase Response, List<T> clientsList)
        {
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Lotes");
            workSheet.Cells[1, 1].LoadFromCollection(clientsList, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=ListagemLotes.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        public void ToExcel_XLSX(HttpResponseBase Response, System.Data.DataTable clientsList, string nome_relatorio)
        {
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Lotes");
            workSheet.Cells[1, 1].LoadFromDataTable(clientsList, true);

            workSheet.Cells.AutoFitColumns();

            var cab = workSheet.Row(1);

            cab.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

            cab.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + nome_relatorio + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        public void ToExcel(HttpResponseBase Response, object clientsList)
        {
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = clientsList;
            grid.DataBind();
            Response.ClearContent();

            //Response.AddHeader("content-disposition", "attachment; filename=Export.xls");
            //Response.ContentType = "application/excel";

            //Response.AddHeader("content-disposition", "attachment;filename=Lotes.xls");
            //Response.AddHeader("Content-Type", "application/vnd.ms-excel");

            //Response.AddHeader("content-disposition", "attachment; filename= LotesExcel.xlsx");
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename= LotesTestes.xlsx");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}