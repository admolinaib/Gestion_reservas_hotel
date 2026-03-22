using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_reservas_hotel.Models
{
    public class HabitacionEstandar : Reserva
    {
        public HabitacionEstandar(string nombre, int habitacion, DateTime fecha, int duracion)
        : base(nombre, habitacion, fecha, duracion)
        {
            TarifaPorNoche = 80000;
        }

        public override double CalcularCostoTotal()
        {    
            return TarifaPorNoche * DuracionEstadia;
        }

        public override void ValidarDatos()
        {
            
        }
    }
}
