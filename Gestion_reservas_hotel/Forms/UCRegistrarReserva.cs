using Gestion_reservas_hotel.Models;
using Gestion_reservas_hotel.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_reservas_hotel
{
    public partial class UCRegistrarReserva : UserControl
    {
        private GestorReservas gestor;
        private Reserva reservaEditar = null;
        public UCRegistrarReserva(GestorReservas gestorReservas)
        {
            InitializeComponent();
            gestor = gestorReservas;
        }
        public UCRegistrarReserva(GestorReservas gestorReservas, Reserva reserva)
        {
            InitializeComponent();
            gestor = gestorReservas;

            reservaEditar = reserva;
        }

        private void UCRegistrarReserva_Load(object sender, EventArgs e)
        {
            cboTipoHabitacion.Items.Clear();
            cboTipoHabitacion.Items.Add("Estándar");
            cboTipoHabitacion.Items.Add("VIP");

            if (reservaEditar != null)
            {
                txtNombre.Text = reservaEditar.NombreCliente;
                txtDocumento.Text = reservaEditar.DocumentoCliente.ToString();
                txtNoches.Text = reservaEditar.DuracionEstadia.ToString();
                dtpFecha.Value = reservaEditar.FechaReserva;

                cboTipoHabitacion.SelectedItem = reservaEditar is HabitacionVIP ? "VIP" : "Estándar";

                foreach (HabitacionDisponible h in listBox1.Items)
                {
                    if (h.Numero == reservaEditar.NumeroHabitacion)
                    {
                        listBox1.SelectedItem = h;
                        break;
                    }
                }
            }

            dtpFecha.MinDate = DateTime.Today;
        }
        private void btnReservar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text;
                DateTime fecha = dtpFecha.Value;
                string tipo = cboTipoHabitacion.Text;

                int duracion;
                if (!int.TryParse(txtNoches.Text, out duracion))
                {
                    MessageBox.Show("Ingrese un número válido de noches");
                    return;
                }

                int documento;
                if (!int.TryParse(txtDocumento.Text, out documento))
                {
                    MessageBox.Show("Ingrese un número de dócumento válido");
                    return;
                }

                if (listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione una habitación");
                    return;
                }

                HabitacionDisponible seleccionada = (HabitacionDisponible)listBox1.SelectedItem;

                int habitacion = seleccionada.Numero;

                Reserva nueva;

                switch (tipo)
                {
                    case "Estándar":
                        nueva = new HabitacionEstandar(nombre, documento, habitacion, fecha, duracion);
                        break;

                    case "VIP":
                        nueva = new HabitacionVIP(nombre, documento, habitacion, fecha, duracion);
                        break;

                    default:
                        MessageBox.Show("Seleccione un tipo de habitación válido");
                        return;
                }

                nueva.DocumentoCliente = documento;

                if (reservaEditar != null)
                {
                    gestor.EliminarReserva(reservaEditar.NumeroHabitacion, reservaEditar.FechaReserva);
                }

                gestor.AgregarReserva(nueva);

                CargarDisponibilidad();
                MessageBox.Show("Reserva agregada correctamente");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar reserva: " + ex.Message);
            }
        }
        private void cboTipoHabitacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDisponibilidad();
        }
        private void txtNoches_TextChanged(object sender, EventArgs e)
        {
            CargarDisponibilidad();
        }
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            CargarDisponibilidad();
        }
        private void CargarDisponibilidad()
        {
            int duracion;

            if (!int.TryParse(txtNoches.Text, out duracion))
                return;

            if (duracion <= 1)
                return;

            if (string.IsNullOrEmpty(cboTipoHabitacion.Text))
                return;

            DateTime fecha = dtpFecha.Value;
            string tipo = cboTipoHabitacion.Text;

            var disponibles = gestor.ObtenerDisponibles(fecha, duracion, tipo);

            listBox1.DataSource = null;
            listBox1.DataSource = disponibles;
        }

        
    }
}
