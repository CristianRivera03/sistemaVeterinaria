using dato.entidades;
using datos;
using System;
using System.Collections.Generic;
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
    }
}