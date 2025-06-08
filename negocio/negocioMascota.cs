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
    public class negocioMascota
    {
        private readonly MascotaRepository _repo = new MascotaRepository();

        public int RegistrarMascota(Mascota mascota)
        {
            return _repo.Insertar(mascota);
        }


        public DataTable ObtenerMascotas()
        {
            return _repo.ObtenerTodos();
        }
    }
}