using System;
using System.Configuration;
using System.Data.SqlClient;
using dato.entidades;

namespace datos
{
    public class ConsultaRepository
    {
        private readonly string _conexionString =
            ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        public int Insertar(consulta c)
        {
            const string sql = @"
              INSERT INTO Consultas
                (IdDueno,
                 IdMascota,
                 FechaHora,
                 Descripcion,
                 Diagnostico,
                 Tratamiento,
                 Veterinario)
              VALUES
                (@IdDueno,
                 @IdMascota,
                 @FechaHora,
                 @Descripcion,
                 @Diagnostico,
                 @Tratamiento,
                 @Veterinario);
              SELECT SCOPE_IDENTITY();
            ";

             var con = new SqlConnection(_conexionString);
             var cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@IdDueno", c.IdDueno);
            cmd.Parameters.AddWithValue("@IdMascota", c.IdMascota);
            cmd.Parameters.AddWithValue("@FechaHora", c.FechaHora);
            cmd.Parameters.AddWithValue("@Descripcion", (object)c.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Diagnostico", (object)c.Diagnostico ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Tratamiento", (object)c.Tratamiento ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Veterinario", (object)c.Veterinario ?? DBNull.Value);

            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
