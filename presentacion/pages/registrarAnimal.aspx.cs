using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using dato.entidades;
using negocio;

namespace presentacion.pages
{
    public partial class registrarAnimal : Page
    {
        private readonly negocioDueno _negocioDue = new negocioDueno();
        private readonly negocioMascota _negocioMasc = new negocioMascota();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Desactivar validación unobtrusive si tienes errores con jQuery
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                // 1) Cargar lista de dueños
                DataTable dt = _negocioDue.ObtenerDuenos();
                ddlDueno.DataSource = dt;
                ddlDueno.DataTextField = "Nombre";
                ddlDueno.DataValueField = "IdDueno";
                ddlDueno.DataBind();
                ddlDueno.Items.Insert(0, new ListItem("-- Seleccione Dueño --", "0"));

                // 2) Fecha de nacimiento por defecto a 01/01/2020
                //    Usamos formato yyyy-MM-dd porque el TextMode="Date" así lo espera
                txtFechaNacimiento.Text = "2020-01-01";
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            // 3) Validaciones
            if (ddlDueno.SelectedValue == "0")
            {
                lblMsg.Text = "Debes elegir un dueño.";
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMsg.Text = "El nombre de la mascota es obligatorio.";
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEspecie.Text))
            {
                lblMsg.Text = "La especie es obligatoria.";
                return;
            }

            // 4) Parseo de fecha con fallback a 2020-01-01
            if (!DateTime.TryParse(txtFechaNacimiento.Text.Trim(), out DateTime fechaNacimiento))
            {
                fechaNacimiento = new DateTime(2020, 1, 1);
            }

            // 5) Crear objeto y llamar a la capa de negocio
            var mascota = new Mascota
            {
                IdDueno = Convert.ToInt32(ddlDueno.SelectedValue),
                Nombre = txtNombre.Text.Trim(),
                Especie = txtEspecie.Text.Trim(),
                Raza = txtRaza.Text.Trim(),
                FechaNacimiento = fechaNacimiento
            };

            int id = _negocioMasc.RegistrarMascota(mascota);
            if (id > 0)
            {
                Response.Redirect("inicio.aspx");
            }
            else
            {
                lblMsg.Text = "Error al guardar la mascota.";
            }
        }
    }
}
