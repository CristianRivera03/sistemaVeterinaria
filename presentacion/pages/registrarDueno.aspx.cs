using System;
using System.Text.RegularExpressions;
using dato.entidades;
using negocio;

namespace presentacion.pages
{
    public partial class registrarDueno : System.Web.UI.Page
    {
        private readonly negocioDueno _negocio = new negocioDueno();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMsg.Text = ""; // Limpiar mensajes previos

            // 1) Validación del nombre (obligatorio)
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMsg.Text = "El nombre es obligatorio.";
                return;
            }

            // Los demás campos son opcionales, así que no los validamos

            // 2) Crear objeto Dueno con lo ingresado
            var dueno = new Dueno
            {
                Nombre = txtNombre.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),   // opcional
                Email = txtEmail.Text.Trim(),      // opcional
                Direccion = txtDireccion.Text.Trim()   // opcional
            };

            // 3) Guardar en base de datos
            int nuevoId = _negocio.RegistrarDueno(dueno);

            if (nuevoId > 0)
                Response.Redirect("inicio.aspx");
            else
                lblMsg.Text = "Error al registrar el dueño. Intenta de nuevo.";
        }

    }
}