using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;
using CapaEntidad.EntidadBoleta;

namespace SistemaVentas
{
    public partial class FrmBoleta : Form
    {
        private Form FormPrincipal;
        //Llamar al mapeado relacional a traves de un objeto
        
        BoletaBL boleta = new BoletaBL();
        EBoleta eBoleta = new EBoleta();





        public FrmBoleta(Form FrmPrincipal)
        {
            InitializeComponent();
            ListarDetalles(null);
            this.FormPrincipal = FrmPrincipal;
            this.FormClosed += new FormClosedEventHandler(FormPrincipal_FrmClosed);

        }
        private void FormPrincipal_FrmClosed(object sender, FormClosedEventArgs e)
        {
            FormPrincipal.Show();
        }
        


        private void ListarDetalles(int ? nroBoleta)
        {

            // Asignar la lista de detalles al DataGridView
            if (nroBoleta.HasValue)
            {
                dgvDetalle.DataSource = boleta.ObtenerDetalles(nroBoleta);
                
            }
            else
            {
                dgvDetalle.DataSource = boleta.ObtenerBoletas();
            }
            
        }
        private void codigo_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            int nroBoleta = Convert.ToInt32(txtNroBoleta.Text.Trim());
            eBoleta.NroBoleta = nroBoleta;
            MessageBox.Show(" "+nroBoleta);
            var resultado =eBoleta.Buscar();
            if (resultado != null)
            {
                txtCantidad.Text = Convert.ToString(resultado.Item2);
                txtCodProducto.Text = Convert.ToString(resultado.Item1);
                txtPrecioUnitario.Text = Convert.ToString(resultado.Item3);
                ListarDetalles(nroBoleta); 
                txtPrecioFinal.Text = Convert.ToString(resultado.Item3);
            }
            else
            {
                MessageBox.Show("Error al buscar la  boleta");
            }
            





        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCliente frm = new FrmCliente(this);
            frm.Show(); 
        }
        
        private void FormBoleta_FrmClosed (object sender, FormClosedEventArgs e)
        {
            FormPrincipal.Show();
        }

        private void FrmBoleta_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerarBoleta_Click(object sender, EventArgs e)
        {
            GenerarBoleta frm = new GenerarBoleta();
            frm.Show();
            this.Hide();
        }
    }
}
