using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_reservas_hotel.Models
{
    public class HabitacionDisponible
    {
        public int Numero { get; set; }
        public double Precio { get; set; }

        public override string ToString()
        {
            return $"Habitación {Numero} - ${Precio}";
        }
    }
}
