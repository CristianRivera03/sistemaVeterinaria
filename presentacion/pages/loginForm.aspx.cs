using System;
using System.Web.Security;
using negocio;

namespace presentacion.pages
{
    public partial class loginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = ""; // Limpia mensajes previos al cargar la página
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContrasenia.Text.Trim();

            // Validación: Usuario y contraseña no pueden estar vacíos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                lblMensaje.Text = "Debe ingresar usuario y contraseña.";
                return;
            }

            // Instanciar la clase de negocio
            var negocio = new negocioUsuario();

            // Verificar credenciales
            int idUsuario = negocio.iniciarSesion(usuario, contraseña);

            if (idUsuario > 0)
            {
                FormsAuthentication.SetAuthCookie(usuario, false);
                Session["Usuario"] = idUsuario;
                Response.Redirect("inicio.aspx");
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos. Intente nuevamente.";
            }
        }
    }
}