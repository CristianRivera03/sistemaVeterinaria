using dato.entidades;
using datos;
using System;
using System.Collections.Generic;
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
    }
}