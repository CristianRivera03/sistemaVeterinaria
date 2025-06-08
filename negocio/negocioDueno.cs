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
    public class negocioDueno
    {
        private readonly datoDueno _repo = new datoDueno();

        public int RegistrarDueno(Dueno dueno)
        {
            return _repo.Insertar(dueno);
        }




        public DataTable ObtenerDuenos() => _repo.ObtenerTodos();


    }
}
