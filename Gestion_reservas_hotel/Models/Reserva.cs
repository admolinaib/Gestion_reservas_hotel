using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_reservas_hotel.Models
{
    public abstract class Reserva
    {
        public string NombreCliente { get; set; }
        public string DocumentoCliente { get; set; }
        public int NumeroHabitacion { get; set; }
        public DateTime FechaReserva { get; set; }
        public int DuracionEstadia { get; set; }
        public double TarifaPorNoche { get; set; }

        public abstract decimal CalcularCostoTotal();


    }
}
