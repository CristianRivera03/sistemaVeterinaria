using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;

namespace presentacion.pages
{
    public partial class loginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContrasenia.Text.Trim();

            // 1) instanciamos la clase de negocio
            var negocio = new negocioUsuario();

            // 2) llamamos al método de instancia
            int idUsuario = negocio.iniciarSesion(usuario, contraseña);

            if (idUsuario > 0)
            {
                Session["Usuario"] = idUsuario;
                Response.Redirect("inicio.aspx");
            }
            else
            {
                //lblMsg.Text = "Usuario o contraseña incorrectos.";
            }
        }

    }
}