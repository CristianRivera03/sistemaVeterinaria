using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using dato.entidades;
using negocio;

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
                  new ListItem("-- Seleccione Dueño --", "0"));

                // Carga lista de mascotas
                DataTable dtMas = _negocioMas.ObtenerMascotas();
                ddlMascota.DataSource     = dtMas;
                ddlMascota.DataTextField  = "Nombre";
                ddlMascota.DataValueField = "IdMascota";
                ddlMascota.DataBind();
                ddlMascota.Items.Insert(0,
                  new ListItem("-- Seleccione Mascota --", "0"));
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
            if (c == null) { Response.Redirect("inicio.aspx"); return; }

            // 2) Crea PDF en memoria
             var ms = new MemoryStream();
            var doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 36, 36, 36, 36);
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, ms);
            doc.Open();

            // 3) Añade contenido
            doc.Add(new iTextSharp.text.Paragraph("Recibo de Consulta") { Alignment = iTextSharp.text.Element.ALIGN_CENTER });
            doc.Add(new iTextSharp.text.Paragraph($"ID Consulta: {c.IdConsulta}"));
            doc.Add(new iTextSharp.text.Paragraph($"Fecha: {c.FechaHora:dd/MM/yyyy HH:mm}"));
            doc.Add(new iTextSharp.text.Paragraph($"Dueño: {c.NombreDueno}"));
            doc.Add(new iTextSharp.text.Paragraph($"Mascota: {c.NombreMascota}"));
            doc.Add(new iTextSharp.text.Paragraph(" "));
            doc.Add(new iTextSharp.text.Paragraph("Descripción:"));
            doc.Add(new iTextSharp.text.Paragraph(c.Descripcion));
            doc.Add(new iTextSharp.text.Paragraph(" "));
            doc.Add(new iTextSharp.text.Paragraph("Diagnóstico:"));
            doc.Add(new iTextSharp.text.Paragraph(c.Diagnostico));
            doc.Add(new iTextSharp.text.Paragraph(" "));
            doc.Add(new iTextSharp.text.Paragraph("Tratamiento:"));
            doc.Add(new iTextSharp.text.Paragraph(c.Tratamiento));
            doc.Add(new iTextSharp.text.Paragraph(" "));
            doc.Add(new iTextSharp.text.Paragraph($"Veterinario: {c.Veterinario}"));

            doc.Close();

            // 4) Envía al cliente
            byte[] bytes = ms.ToArray();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=ReciboConsulta.pdf");
            Response.BinaryWrite(bytes);
            Response.End();
        }


        protected void btnBackInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }
    }
}
