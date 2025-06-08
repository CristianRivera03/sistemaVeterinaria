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

            // Validación del nombre (campo obligatorio)
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMsg.Text = "El nombre es obligatorio.";
                return;
            }

            // Validación de teléfono (exactamente 8 dígitos)
            if (!Regex.IsMatch(txtTelefono.Text, @"^\d{8}$"))
            {
                lblMsg.Text = "El teléfono debe contener exactamente 8 dígitos.";
                return;
            }

            // Validación de correo electrónico (formato válido)
            if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                lblMsg.Text = "Ingrese un correo electrónico válido.";
                return;
            }

            // Crear objeto Dueno
            var dueno = new Dueno
            {
                Nombre = txtNombre.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Direccion = txtDireccion.Text.Trim()
            };

            // Guardar en base de datos
            int nuevoId = _negocio.RegistrarDueno(dueno);

            if (nuevoId > 0)
                Response.Redirect("inicio.aspx");
            else
                lblMsg.Text = "Error al registrar el dueño. Intenta de nuevo.";
        }
    }
}