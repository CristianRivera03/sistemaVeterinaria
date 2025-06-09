using dato.entidades;
using datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class negocioConsulta
    {
        private readonly ConsultaRepository _repo = new ConsultaRepository();

        public int RegistrarConsulta(consulta c)
        {
            return _repo.Insertar(c);
        }

        public DataTable ObtenerPorIdComprobante(int idConsulta)
        {
            return new ConsultaRepository().ObtenerPorIdComprobante(idConsulta);
        }


        public DataTable ObtenerConsultasPorFecha(DateTime desde, DateTime hasta)
        {
            return new ConsultaRepository().ObtenerPorFecha(desde, hasta);
        }

        public consulta ObtenerConsultaPorId(int id) => _repo.ObtenerPorId(id);
    }
}