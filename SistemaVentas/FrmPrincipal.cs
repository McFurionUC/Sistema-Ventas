using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
           
        
        }

        private void FrmPrincipal_load(object sender,EventArgs e )
        {
           
            
        }

        private void btnCasa_Click(object sender, EventArgs e)
        {
            FrmBoleta frmBoleta = new FrmBoleta(this);

            frmBoleta.Show();
            this.Hide();
        }

        private void btnBoleta_Click(object sender, EventArgs e)
        {
            FrmBoleta frmBoleta = new FrmBoleta(this);

            frmBoleta.Show();
            this.Hide();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            FrmCliente frmCliente = new FrmCliente(this);

            frmCliente.Show();
            this.Hide();
        }
    }
}
