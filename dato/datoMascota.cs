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
            cmd.Parameters.AddWithValue("@FechaNacimiento", m.FechaNacimiento);

            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
