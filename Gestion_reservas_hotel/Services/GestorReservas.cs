using Gestion_reservas_hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_reservas_hotel.Services
{
    public class GestorReservas
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

        public void EliminarReserva(int numeroHabitacion, DateTime fecha)
        {
            Reserva reserva = reservas
                .FirstOrDefault(r => r.NumeroHabitacion == numeroHabitacion && r.FechaReserva == fecha);

            if (reserva == null)
                throw new Exception("La reserva no existe");

            reservas.Remove(reserva);
        }

        public void EditarReserva(Reserva actualizada)
        {
            Reserva existente = reservas
                .FirstOrDefault(r => r.NumeroHabitacion == actualizada.NumeroHabitacion
                                  && r.FechaReserva == actualizada.FechaReserva);

            if (existente == null)
                throw new Exception("La reserva no existe");

            reservas.Remove(existente);
            reservas.Add(actualizada);
        }

        public List<HabitacionDisponible> ObtenerDisponibles(DateTime fecha, int duracion, string tipo)
        {
            List<int> todas = new List<int> { 101, 102, 103, 201, 202 };

            List<HabitacionDisponible> disponibles = new List<HabitacionDisponible>();

            foreach (int num in todas)
            {
                bool ocupada = false;

                foreach (Reserva r in reservas)
                {
                    bool mismaHabitacion = r.NumeroHabitacion == num;

                    DateTime finExistente = r.FechaReserva.AddDays(r.DuracionEstadia);
                    DateTime finNueva = fecha.AddDays(duracion);

                    bool seCruzan =
                        fecha < finExistente &&
                        r.FechaReserva < finNueva;

                    if (mismaHabitacion && seCruzan)
                    {
                        ocupada = true;
                        break;
                    }
                }

                if (!ocupada)
                {
                    Reserva temp;
                        
                    if (tipo == "Estándar")
                        temp = new HabitacionEstandar("temp", 0, num, fecha, duracion);
                    else
                        temp = new HabitacionVIP("temp", 0, num, fecha, duracion);

                    double precio = temp.CalcularCostoTotal();

                    disponibles.Add(new HabitacionDisponible
                    {
                        Numero = num,
                        Precio = precio
                    });
                }
            }

            return disponibles;
        }
    }
}
