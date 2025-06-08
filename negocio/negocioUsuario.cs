using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using datos;
using static datos.Usuarios;

namespace negocio
{
    public class negocioUsuario
    {
        Usuarios usuarios = new Usuarios();

        public int iniciarSesion(string usuario, string contraseña)
        {
            return usuarios.validarUsuario(usuario, contraseña);
        }

        public DataTable ObtenerUsuarios()
        {
            return usuarios.ObtenerUsuarios();
        }
    }
}
