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

namespace Gestion_reservas_hotel.Forms
{
    public partial class UCConsultarReservas : UserControl
    {
        private GestorReservas gestor;
        public UCConsultarReservas(GestorReservas gestorReservas)
        {
            InitializeComponent();
            gestor = gestorReservas;

            dgvReservas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReservas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservas.MultiSelect = false;
            dgvReservas.DefaultCellStyle.ForeColor = Color.Black;
            dgvReservas.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvReservas.DefaultCellStyle.BackColor = Color.White;
            dgvReservas.DefaultCellStyle.SelectionBackColor = Color.LightGray;
        }
        private void UCConsultarReservas_Load(object sender, EventArgs e)
        {
            cboFiltroTipo.Items.Add("Todos");
            cboFiltroTipo.Items.Add("Estándar");
            cboFiltroTipo.Items.Add("VIP");

            cboFiltroTipo.SelectedIndex = 0;

            CargarReservas();
        }
        private void CargarReservas()
        {
            dgvReservas.DataSource = null;
            dgvReservas.DataSource = null;

            dgvReservas.DataSource = gestor.ObtenerReservas().ToList();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvReservas.CurrentRow == null)
                return;

            Reserva seleccionada = (Reserva)dgvReservas.CurrentRow.DataBoundItem;

            gestor.EliminarReserva(seleccionada.NumeroHabitacion, seleccionada.FechaReserva);

            CargarReservas();
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.ToLower();

            dgvReservas.DataSource = null;

            dgvReservas.DataSource = gestor.ObtenerReservas()
                .Where(r => r.NombreCliente.ToLower().Contains(filtro))
                .ToList();
        }
        private void cboFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo = cboFiltroTipo.Text;

            List<Reserva> lista = gestor.ObtenerReservas();

            if (tipo == "Estándar")
                lista = lista.Where(r => r is HabitacionEstandar).ToList();
            else if (tipo == "VIP")
                lista = lista.Where(r => r is HabitacionVIP).ToList();

            dgvReservas.DataSource = null;
            dgvReservas.DataSource = lista.ToList();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvReservas.CurrentRow == null)
                return;

            Reserva seleccionada = (Reserva)dgvReservas.CurrentRow.DataBoundItem;

            Form parent = this.FindForm();

            if (parent is MainForm main)
            {
                UCRegistrarReserva uc = new UCRegistrarReserva(gestor, seleccionada);

                main.pnlContenedor.Controls.Clear();
                uc.Dock = DockStyle.Fill;
                main.pnlContenedor.Controls.Add(uc);
            }
        }
        private void cboOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Reserva> lista = gestor.ObtenerReservas();

            if (cboOrden.Text == "A-Z")
                lista = lista.OrderBy(r => r.NombreCliente).ToList();

            else if (cboOrden.Text == "Z-A")
                lista = lista.OrderByDescending(r => r.NombreCliente).ToList();

            dgvReservas.DataSource = null;
            dgvReservas.DataSource = lista.ToList();
        }
    }
}
