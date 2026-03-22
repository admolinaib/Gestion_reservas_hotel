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

        protected Reserva(string nombre, int habitacion, DateTime fecha, int duracion)
        {
            NombreCliente = nombre;
            NumeroHabitacion = habitacion;
            FechaReserva = fecha;
            DuracionEstadia = duracion;

            ValidarDatos();
        }
        public abstract double CalcularCostoTotal();

        public virtual double AplicarDescuento(double total)
        {
            return total;
        }

        public virtual void ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(NombreCliente))
                throw new Exception("El nombre es obligatorio");

            if (NumeroHabitacion <= 0)
                throw new Exception("Número de habitación inválido");

            if (DuracionEstadia <= 1)
                throw new Exception("La estadía debe ser mayor a 1 noche");

            if (FechaReserva == default)
                throw new Exception("Fecha inválida");
        }
    }
}
