using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using dato.entidades;

namespace datos
{
    public class ConsultaRepository
    {
        private readonly string _conexionString =
            ConfigurationManager.ConnectionStrings["con"].ConnectionString;



        // datos/ConsultaRepository.cs
        public DataTable ObtenerPorFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            var dt = new DataTable();
            const string sql = @"
              SELECT c.IdConsulta,
                     d.Nombre   AS Dueño,
                     m.Nombre   AS Mascota,
                     c.FechaHora,
                     c.Descripcion,
                     c.Diagnostico,
                     c.Tratamiento,
                     c.Veterinario
                FROM Consultas c
                JOIN Duenos d      ON c.IdDueno   = d.IdDueno
                JOIN Mascotas m    ON c.IdMascota = m.IdMascota
               WHERE c.FechaHora BETWEEN @desde AND @hasta
               ORDER BY c.FechaHora;
            ";

             var con = new SqlConnection(_conexionString);
             var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@desde", fechaDesde.Date);
            cmd.Parameters.AddWithValue("@hasta", fechaHasta.Date.AddDays(1).AddSeconds(-1));

             var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }


        public DataTable ObtenerPorIdComprobante(int idConsulta)
        {
            var dt = new DataTable();
            const string sql = @"
            SELECT c.IdConsulta,
             d.Nombre   AS Dueño,
             m.Nombre   AS Mascota,
             c.FechaHora,
             c.Descripcion,
             c.Diagnostico,
             c.Tratamiento,
             c.Veterinario
                FROM Consultas c
                JOIN Duenos d   ON c.IdDueno   = d.IdDueno
                JOIN Mascotas m ON c.IdMascota = m.IdMascota
               WHERE c.IdConsulta = @idConsulta;
            ";
             var con = new SqlConnection(_conexionString);
             var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@idConsulta", idConsulta);
             var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }





        public consulta ObtenerPorId(int id)
        {
            const string sql = @"
      SELECT c.*, d.Nombre AS NombreDueno, m.Nombre AS NombreMascota
        FROM Consultas c
        JOIN Duenos d    ON d.IdDueno    = c.IdDueno
        JOIN Mascotas m  ON m.IdMascota  = c.IdMascota
       WHERE c.IdConsulta = @IdConsulta;
    ";
             var con = new SqlConnection(_conexionString);
             var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@IdConsulta", id);
            con.Open();
            var rdr = cmd.ExecuteReader();
            if (!rdr.Read()) return null;

            return new consulta
            {
                IdConsulta = id,
                IdDueno = (int)rdr["IdDueno"],
                IdMascota = (int)rdr["IdMascota"],
                FechaHora = (DateTime)rdr["FechaHora"],
                Descripcion = rdr["Descripcion"]?.ToString(),
                Diagnostico = rdr["Diagnostico"]?.ToString(),
                Tratamiento = rdr["Tratamiento"]?.ToString(),
                Veterinario = rdr["Veterinario"]?.ToString(),

                // propiedades extra en tu clase Consulta:
                NombreDueno = rdr["NombreDueno"].ToString(),
                NombreMascota = rdr["NombreMascota"].ToString()
            };
        }





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
