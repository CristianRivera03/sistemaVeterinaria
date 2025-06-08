using System;
using System.Data;
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
            if (ddlDueno.SelectedValue == "0")
            {
                lblMsg.Text = "Debe elegir un dueño.";
                return;
            }

            if (ddlMascota.SelectedValue == "0")
            {
                lblMsg.Text = "Debe elegir una mascota.";
                return;
            }

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

            int id = _negocioCon.RegistrarConsulta(consulta);
            if (id > 0)
                Response.Redirect("inicio.aspx");
            else
                lblMsg.Text = "Error al registrar la consulta.";
        }
    }
}
