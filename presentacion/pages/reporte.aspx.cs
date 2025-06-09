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
using System.Web.UI.WebControls;
using iTextSharp.text.pdf.draw;


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

        private void GenerarComprobantePdf(int idConsulta)
        {
            // 1) Traer datos de la consulta
            DataTable dt = _negocioCon.ObtenerPorIdComprobante(idConsulta);
            if (dt.Rows.Count == 0) return;
            var row = dt.Rows[0];

            // 2) Preparar memoria y documento
             var ms = new MemoryStream();
            var doc = new Document(PageSize.A4, 40, 40, 60, 40);
            var writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            // 3) Logo + Encabezado
            //   Asegúrate de tener un logo en "~/resources/logo.png"
            //string logoPath = Server.MapPath("~/resources/logo.png");
            //if (File.Exists(logoPath))
            //{
            //    var logo = Image.GetInstance(logoPath);
            //    logo.ScaleToFit(100f, 100f);
            //    logo.Alignment = Element.ALIGN_LEFT;
            //    doc.Add(logo);
            //}

            var tituloFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            var subFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
            var title = new Paragraph($"Comprobante de Consulta #{idConsulta}", tituloFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 10f
            };
            doc.Add(title);

            // Línea separadora
            var line = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -5);
            doc.Add(new Chunk(line));

            // 4) Datos generales
            var infoTable = new PdfPTable(2) { WidthPercentage = 80f, SpacingBefore = 10f, SpacingAfter = 20f };
            infoTable.SetWidths(new float[] { 1f, 2f });

            void AddCell(string label, string value)
            {
                var labFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
                var valFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);
                infoTable.AddCell(new PdfPCell(new Phrase(label, labFont)) { Border = 0 });
                infoTable.AddCell(new PdfPCell(new Phrase(value, valFont)) { Border = 0 });
            }

            AddCell("Dueño:", row["Dueño"].ToString());
            AddCell("Mascota:", row["Mascota"].ToString());
            AddCell("Fecha:", Convert.ToDateTime(row["FechaHora"]).ToString("dd/MM/yyyy HH:mm"));
            doc.Add(infoTable);

            // 5) Detalles clínicos en tabla bonita
            var detTable = new PdfPTable(2) { WidthPercentage = 100f };
            detTable.SetWidths(new float[] { 1f, 3f });
            detTable.SpacingBefore = 10f;

            var hdrFont = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.WHITE);
            var cellFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);

            // cabeceras
            BaseColor headerBg = new BaseColor(79, 129, 189); // azul suave
            void AddHeader(string text)
            {
                detTable.AddCell(new PdfPCell(new Phrase(text, hdrFont))
                {
                    BackgroundColor = headerBg,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 5
                });
            }

            // valores
            void AddValue(string text)
            {
                detTable.AddCell(new PdfPCell(new Phrase(text, cellFont))
                {
                    Padding = 5
                });
            }

            AddHeader("Descripción");
            AddValue(row["Descripcion"].ToString());
            AddHeader("Diagnóstico");
            AddValue(row["Diagnostico"].ToString());
            AddHeader("Tratamiento");
            AddValue(row["Tratamiento"].ToString());
            AddHeader("Veterinario");
            AddValue(row["Veterinario"].ToString());

            doc.Add(detTable);

            // 6) Pie de página (opcional)
            var footer = new Paragraph("¡Gracias por confiar en Clínica Veterinaria Giron!", subFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 20f
            };
            doc.Add(footer);

            doc.Close();

            // 7) Enviar PDF al cliente
            byte[] pdfBytes = ms.ToArray();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition",
                $"attachment;filename=Comprobante_{idConsulta}.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.End();
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

        protected void gvReporte_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GenerarComprobante")
            {
                int idConsulta = Convert.ToInt32(e.CommandArgument);
                GenerarComprobantePdf(idConsulta);
            }
        }



        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");

        }
    }
}
