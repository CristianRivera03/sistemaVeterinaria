using dato.entidades;
using negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion.pages
{
    public partial class registrarAnimal : System.Web.UI.Page
    {
        private readonly negocioDueno _negocioDue = new negocioDueno();
        private readonly negocioMascota _negocioMasc = new negocioMascota();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Desactivar validación unobtrusive para evitar errores con jQuery
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                DataTable dt = _negocioDue.ObtenerDuenos();
                ddlDueno.DataSource = dt;
                ddlDueno.DataTextField = "Nombre";
                ddlDueno.DataValueField = "IdDueno";
                ddlDueno.DataBind();

                // Opción: inserta un ítem "placeholder"
                ddlDueno.Items.Insert(0, new ListItem("-- Seleccione Dueño --", "0"));
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMsg.Text = ""; // Limpiar mensajes previos

            // Validación: el dueño debe ser seleccionado
            if (ddlDueno.SelectedValue == "0")
            {
                lblMsg.Text = "Debes elegir un dueño.";
                return;
            }

            // Validación: el nombre de la mascota no puede estar vacío
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMsg.Text = "El nombre de la mascota es obligatorio.";
                return;
            }

            // Validación: la especie no puede estar vacía
            if (string.IsNullOrWhiteSpace(txtEspecie.Text))
            {
                lblMsg.Text = "La especie es obligatoria.";
                return;
            }

            // Validación: fecha de nacimiento debe ser válida
            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento))
            {
                lblMsg.Text = "La fecha de nacimiento no es válida.";
                return;
            }

            var m = new Mascota
            {
                IdDueno = int.Parse(ddlDueno.SelectedValue),
                Nombre = txtNombre.Text.Trim(),
                Especie = txtEspecie.Text.Trim(),
                Raza = txtRaza.Text.Trim(), // Puede estar vacío sin problema
                FechaNacimiento = fechaNacimiento
            };

            int id = _negocioMasc.RegistrarMascota(m);
            if (id > 0)
                Response.Redirect("inicio.aspx");
            else
                lblMsg.Text = "Error al guardar la mascota.";
        }
    }
}