using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using dato.entidades;

namespace datos
{
    public class datoDueno
    {
        string conexionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        public DataTable ObtenerTodos()
        {
            var dt = new DataTable();
             var con = new SqlConnection(conexionString);
             var da = new SqlDataAdapter(
              "SELECT IdDueno, Nombre FROM Duenos ORDER BY Nombre", con);
            da.Fill(dt);
            return dt;
        }



        public int Insertar(Dueno dueno)
        {
            const string sql = @"
                INSERT INTO Duenos (Nombre, Telefono, Email, Direccion)
                VALUES (@Nombre, @Telefono, @Email, @Direccion);
                SELECT SCOPE_IDENTITY();
            ";

             var con = new SqlConnection(conexionString);
             var cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@Nombre", dueno.Nombre);
            cmd.Parameters.AddWithValue("@Telefono", (object)dueno.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object)dueno.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Direccion", (object)dueno.Direccion ?? DBNull.Value);

            con.Open();
            object result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }
    }
}
