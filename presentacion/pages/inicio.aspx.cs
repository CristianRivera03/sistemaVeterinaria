using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion.pages
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAnimal_Click(object sender, EventArgs e)
        {
            Response.Redirect("registrarAnimal.aspx");
        }

        protected void btnDueno_Click(object sender, EventArgs e)
        {
            Response.Redirect("registrarDueno.aspx");
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            Response.Redirect("registrarConsulta.aspx");
        }

        protected void btnReportes_Click(object sender, EventArgs e)
        {
            Response.Redirect("reporte.aspx");
        }
    }
}