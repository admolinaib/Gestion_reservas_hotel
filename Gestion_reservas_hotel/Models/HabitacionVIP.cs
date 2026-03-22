using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_reservas_hotel.Models
{
    public class HabitacionVIP : Reserva
    {
        public HabitacionVIP(string nombre, int idCliente, int habitacion, DateTime fecha, int duracion)
        : base(nombre, idCliente, habitacion, fecha, duracion)
        {
            TarifaPorNoche = 150000;
        }

        public override double CalcularCostoTotal()
        {
            double total = TarifaPorNoche * DuracionEstadia;

            return AplicarDescuento(total);
        }

        public override double AplicarDescuento(double total)
        {
            if (DuracionEstadia > 5)
                return total - (total * 0.20);

            return total;
        }
    }
}
