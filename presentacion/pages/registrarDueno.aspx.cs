using System;
using dato.entidades;     // Donde esté tu clase Dueno
using negocio;            // Donde esté tu clase negocioDueno

namespace presentacion.pages
{
    public partial class registrarDueno : System.Web.UI.Page
    {
        // 1) Crea aquí la instancia de la capa de negocio
        private readonly negocioDueno _negocio = new negocioDueno();

        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // 2) Generas el objeto Dueno
            var dueno = new Dueno
            {
                Nombre = txtNombre.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Direccion = txtDireccion.Text.Trim()
            };

            // 3) Llamas al método de instancia (_negocio), no a la clase
            int nuevoId = _negocio.RegistrarDueno(dueno);

            if (nuevoId > 0)
                Response.Redirect("inicio.aspx");
            else
                lblMsg.Text = "Error al registrar el dueño. Intenta de nuevo.";
        }
    }
}
