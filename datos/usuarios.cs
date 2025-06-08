using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;


namespace datos
{
    public class usuarios
    {
        public class Usuarios
        {
            string conexionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            public int validarUsuario(string usuario, string contraseña)
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    con.Open();
                    using (SqlCommand consulta = new SqlCommand("SELECT idUsuario FROM usuario WHERE usuario = @usuario AND contraseña = @contraseña", con))
                    {
                        consulta.Parameters.AddWithValue("@usuario", usuario);
                        consulta.Parameters.AddWithValue("@contraseña", contraseña);

                        object resultado = consulta.ExecuteScalar();
                        return resultado != null ? Convert.ToInt32(resultado) : -1;
                    }
                }
            }

            public DataTable ObtenerUsuarios()
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT idUsuario, usuario FROM usuarios", con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                return dt;
            }

        }
    }
}
