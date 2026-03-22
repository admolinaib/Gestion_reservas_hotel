using Gestion_reservas_hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_reservas_hotel.Services
{
    internal class GestorReservas
    {
        private List<Reserva> reservas = new List<Reserva>();

        public void AgregarReserva(Reserva nueva)
        {
            ValidarDisponibilidad(nueva);
            reservas.Add(nueva);
        }

        private void ValidarDisponibilidad(Reserva nueva)
        {
            foreach (Reserva r in reservas)
            {
                bool mismaHabitacion = r.NumeroHabitacion == nueva.NumeroHabitacion;

                DateTime finExistente = r.FechaReserva.AddDays(r.DuracionEstadia);
                DateTime finNueva = nueva.FechaReserva.AddDays(nueva.DuracionEstadia);

                bool seCruzan =
                    nueva.FechaReserva < finExistente &&
                    r.FechaReserva < finNueva;

                if (mismaHabitacion && seCruzan)
                {
                    throw new Exception("La habitación ya está reservada en esas fechas");
                }
            }
        }
    }
}
