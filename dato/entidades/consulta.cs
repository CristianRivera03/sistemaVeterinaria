using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dato.entidades
{
    public class consulta
    {

        public int IdConsulta { get; set; }
        public int IdDueno { get; set; }
        public int IdMascota { get; set; }
        public DateTime FechaHora { get; set; }
        public string Descripcion { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string Veterinario { get; set; }
        public string NombreDueno { get; set; }
        public string NombreMascota { get; set; }

    }
}
