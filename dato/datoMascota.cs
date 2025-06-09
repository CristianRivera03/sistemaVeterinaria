using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using dato.entidades;

namespace datos
{
    public class MascotaRepository
    {
        private readonly string _conexionString =
            ConfigurationManager.ConnectionStrings["con"].ConnectionString;




        public DataTable ObtenerTodos()
        {
            var dt = new DataTable();
            const string sql = @"
                SELECT IdMascota, Nombre
                  FROM Mascotas
                 ORDER BY Nombre;
            ";

            using (var con = new SqlConnection(_conexionString))
            using (var cmd = new SqlCommand(sql, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            return dt;
        }
        public int Insertar(Mascota m)
        {
            const string sql = @"
        INSERT INTO Mascotas
          (IdDueno, Nombre, Especie, Raza, FechaNacimiento)
        VALUES
          (@IdDueno, @Nombre, @Especie, @Raza, @FechaNacimiento);
        SELECT SCOPE_IDENTITY();
    ";

             var con = new SqlConnection(_conexionString);
             var cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@IdDueno", m.IdDueno);
            cmd.Parameters.AddWithValue("@Nombre", m.Nombre);
            cmd.Parameters.AddWithValue("@Especie", (object)m.Especie ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Raza", (object)m.Raza ?? DBNull.Value);
            // Si es null, pasa DBNull.Value; si no, el DateTime
            cmd.Parameters.AddWithValue("@FechaNacimiento", (object)m.FechaNacimiento ?? DBNull.Value);

            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

    }
}
