using System;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Web;
using System.Web.UI;// EPPlus para Excel
using iTextSharp.text;         // iTextSharp para PDF
using iTextSharp.text.pdf;
using negocio;
using ClosedXML.Excel;


namespace presentacion.pages
    {
        public partial class reporte : Page
        {
            private readonly negocioConsulta _negocioCon = new negocioConsulta();

            protected void btnBuscar_Click(object sender, EventArgs e)
            {
                if (DateTime.TryParse(txtDesde.Text, out DateTime desde) &&
                    DateTime.TryParse(txtHasta.Text, out DateTime hasta))
                {
                    DataTable dt = _negocioCon.ObtenerConsultasPorFecha(desde, hasta);
                    gvReporte.DataSource = dt;
                    gvReporte.DataBind();
                    Session["ReporteConsultas"] = dt;
            }
            }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            var dt = Session["ReporteConsultas"] as DataTable;
            if (dt == null || dt.Rows.Count == 0) return;

             var wb = new XLWorkbook();
            wb.Worksheets.Add(dt, "Consultas");

            Response.Clear();
            Response.ContentType =
              "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition",
              "attachment;filename=ReporteConsultas.xlsx");

             var ms = new MemoryStream();
            wb.SaveAs(ms);
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }



        protected void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(txtDesde.Text, out DateTime desde) ||
                !DateTime.TryParse(txtHasta.Text, out DateTime hasta))
            {
                //lblMsg.Text = "Rango de fechas inválido.";
                return;
            }

            // Re-obtenemos los datos
            DataTable dt = _negocioCon.ObtenerConsultasPorFecha(desde, hasta);
            if (dt.Rows.Count == 0)
            {
                //lblMsg.Text = "No hay datos para exportar.";
                return;
            }

            // Genera el PDF con dt (igual que antes)
            var ms = new MemoryStream();
            var doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 10, 10);
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, ms);
            doc.Open();

            var table = new iTextSharp.text.pdf.PdfPTable(dt.Columns.Count);
            foreach (DataColumn col in dt.Columns)
                table.AddCell(new iTextSharp.text.Phrase(col.ColumnName));
            foreach (DataRow row in dt.Rows)
                foreach (var cell in row.ItemArray)
                    table.AddCell(new iTextSharp.text.Phrase(cell?.ToString() ?? ""));

            doc.Add(table);
            doc.Close();

            byte[] pdfBytes = ms.ToArray();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteConsultas.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.End();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");

        }
    }
}
