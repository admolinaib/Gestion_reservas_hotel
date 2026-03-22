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

        public UCRegistrarReserva(GestorReservas gestorReservas)
        {
            InitializeComponent();
            gestor = gestorReservas;
        }

        private void UCRegistrarReserva_Load(object sender, EventArgs e)
        {
            cboTipoHabitacion.Items.Add("Estándar");
            cboTipoHabitacion.Items.Add("VIP");
        }

        private void button1_Click(object sender, EventArgs e)
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

                Reserva nueva;

                switch (tipo)
                {
                    case "Estándar":
                        nueva = new HabitacionEstandar(nombre, documento, fecha, duracion);
                        break;

                    case "VIP":
                        nueva = new HabitacionVIP(nombre, documento, fecha, duracion);
                        break;

                    default:
                        MessageBox.Show("Seleccione un tipo de habitación válido");
                        return;
                }

                nueva.DocumentoCliente = documento;

                gestor.AgregarReserva(nueva);

                MessageBox.Show("Reserva agregada correctamente");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
