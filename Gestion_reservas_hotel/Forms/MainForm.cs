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
    public partial class MainForm : Form
    {
        private GestorReservas gestor = new GestorReservas();
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UCRegistrarReserva uc = new UCRegistrarReserva(gestor);
            pnlContenedor.Controls.Clear();
            pnlContenedor.Controls.Add(uc);
        }
    }
}
