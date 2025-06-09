using System.Configuration;
using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using dato.entidades;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf;
using iTextSharp.text;
using negocio;
using WebListItem = System.Web.UI.WebControls.ListItem;


namespace presentacion.pages
{
    public partial class registrarConsulta : System.Web.UI.Page
    {
        private readonly negocioDueno     _negocioDue = new negocioDueno();
        private readonly negocioMascota   _negocioMas = new negocioMascota();
        private readonly negocioConsulta  _negocioCon = new negocioConsulta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carga lista de dueños
                DataTable dtDue = _negocioDue.ObtenerDuenos();
                ddlDueno.DataSource     = dtDue;
                ddlDueno.DataTextField  = "Nombre";
                ddlDueno.DataValueField = "IdDueno";
                ddlDueno.DataBind();
                ddlDueno.Items.Insert(0,
                    new WebListItem("-- Seleccione Dueño --", "0"));


                // Carga lista de mascotas
                DataTable dtMas = _negocioMas.ObtenerMascotas();
                ddlMascota.DataSource     = dtMas;
                ddlMascota.DataTextField  = "Nombre";
                ddlMascota.DataValueField = "IdMascota";
                ddlMascota.DataBind();
                ddlMascota.Items.Insert(0,
                    new WebListItem("-- Seleccione Mascota --", "0"));
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
            => Response.Redirect("inicio.aspx");

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // validaciones...
            var consulta = new consulta
            {
                IdDueno      = int.Parse(ddlDueno.SelectedValue),
                IdMascota    = int.Parse(ddlMascota.SelectedValue),
                FechaHora    = DateTime.Parse(txtFechaHora.Text),
                Descripcion  = txtDescripcion.Text.Trim(),
                Diagnostico  = txtDiagnostico.Text.Trim(),
                Tratamiento  = txtTratamiento.Text.Trim(),
                Veterinario  = txtVeterinario.Text.Trim()
            };

            int nuevoId = _negocioCon.RegistrarConsulta(consulta);
            if (nuevoId > 0)
            {
                // guardamos el Id y lanzamos el modal
                hfConsultaId.Value = nuevoId.ToString();
                Page.ClientScript.RegisterStartupScript(
                 GetType(),
                 "showModal",
                 @"
                  var myModal = new bootstrap.Modal(
                    document.getElementById('confirmModal')
                  );
                  myModal.show();
                ",
                 true
             );

            }
            else
            {
                lblMsg.Text = "Error al registrar la consulta.";
            }
        }


        protected void btnGeneratePdf_Click(object sender, EventArgs e)
        {
            int id = int.Parse(hfConsultaId.Value);
            // 1) Recupera datos completos
            consulta c = _negocioCon.ObtenerConsultaPorId(id);
            if (c == null)
            {
                Response.Redirect("inicio.aspx");
                return;
            }

            // 2) Crea PDF en memoria con márgenes
             var ms = new MemoryStream();
            var doc = new iTextSharp.text.Document(PageSize.A4, 40, 40, 60, 40);
            PdfWriter.GetInstance(doc, ms);
            doc.Open();

            // 3) Título centrado
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            var title = new Paragraph($"Recibo de Consulta #{c.IdConsulta}", titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 10f
            };
            doc.Add(title);

            // 4) Línea separadora
            var line = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -5);
            doc.Add(new Chunk(line));

            // 5) Tabla de datos generales (2 columnas, sin bordes)
            var infoTable = new PdfPTable(2)
            {
                WidthPercentage = 80f,
                SpacingBefore = 10f,
                SpacingAfter = 20f
            };
            infoTable.SetWidths(new float[] { 1f, 2f });

            void AddInfo(string label, string value)
            {
                var labFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
                var valFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);
                infoTable.AddCell(new PdfPCell(new Phrase(label, labFont)) { Border = 0 });
                infoTable.AddCell(new PdfPCell(new Phrase(value, valFont)) { Border = 0 });
            }

            AddInfo("Dueño:", c.NombreDueno);
            AddInfo("Mascota:", c.NombreMascota);
            AddInfo("FechaHora:", c.FechaHora.ToString("dd/MM/yyyy HH:mm"));
            doc.Add(infoTable);

            // 6) Tabla de detalles clínicos (cabecera azul, texto normal)
            var detTable = new PdfPTable(2)
            {
                WidthPercentage = 100f,
                SpacingBefore = 10f
            };
            detTable.SetWidths(new float[] { 1f, 3f });

            var hdrFont = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.WHITE);
            var cellFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            BaseColor headerBg = new BaseColor(79, 129, 189);

            void AddHeader(string text)
            {
                detTable.AddCell(new PdfPCell(new Phrase(text, hdrFont))
                {
                    BackgroundColor = headerBg,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 5
                });
            }

            void AddValue(string text)
            {
                detTable.AddCell(new PdfPCell(new Phrase(text, cellFont))
                {
                    Padding = 5
                });
            }

            AddHeader("Descripción");
            AddValue(c.Descripcion);
            AddHeader("Diagnóstico");
            AddValue(c.Diagnostico);
            AddHeader("Tratamiento");
            AddValue(c.Tratamiento);
            AddHeader("Veterinario");
            AddValue(c.Veterinario);

            doc.Add(detTable);

            // 7) Pie de página
            var footerFont = FontFactory.GetFont("Arial", 12, Font.ITALIC);
            var footer = new Paragraph("¡Gracias por confiar en Clínica Veterinaria Giron!", footerFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 20f
            };
            doc.Add(footer);

            doc.Close();

            // 8) Envío al cliente
            byte[] bytes = ms.ToArray();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", $"attachment; filename=ReciboConsulta_{c.IdConsulta}.pdf");
            Response.BinaryWrite(bytes);
            Response.End();
        }


        protected void btnBackInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }
    }
}
