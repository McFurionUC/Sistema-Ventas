using CapaBaseDatos;
using CapaEntidad;
using CapaEntidad.EntidadBoleta;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas
{
    public partial class GenerarBoleta : Form
    {
        EBoleta eBoleta = new  EBoleta();
        BoletaBL boleta = new BoletaBL();
        public GenerarBoleta()
        {
            InitializeComponent();
            ListarComboBox();
            ListarBoleta();
            lblNroBoleta.Text = NroBoleta().ToString();

        }
        //Llenar comboBox
        public  void ListarComboBox()
        {
            List<string> categorias = eBoleta.ObtenerCategoria();
            cmCategoria.DataSource = categorias;
            List<string> vendedor = eBoleta.ObtenerVendedor();
            cmVendedor.DataSource = vendedor;
            List<string> cliente = eBoleta.ObtenerCliente();
            cmCliente.DataSource = cliente;
            List<string> producto = eBoleta.ObtenerProducto();
            cmProducto.DataSource = producto;
        }
  

        public int NroBoleta()
        {
            int dato = eBoleta.UltimoNroBoleta();
            return dato;
        }
        public void ListarBoleta()

        {
            dgvVentas.DataSource = boleta.ObtenerDetalleProductos();
        }

        public void ListarVenta()
        {
            
            dgvVentas.DataSource = boleta.ObtenerDetalleVenta(Convert.ToInt32(lblNroBoleta.Text));
        }
       

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(cmCategoria.Text) ||
                string.IsNullOrWhiteSpace(cmCliente.Text) ||
                string.IsNullOrWhiteSpace(cmVendedor.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                string.IsNullOrWhiteSpace(cmProducto.Text) 
                
                )
            {
                MessageBox.Show("Todos los campos deben ser llenados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si algún campo está vacío
            }
            else
            {
                

               
                    eBoleta.CodCliente = cmCliente.Text;
                    eBoleta.CodVendedor = cmVendedor.Text;
                    eBoleta.CodProducto = cmProducto.Text;
                    eBoleta.NroBoleta = Convert.ToInt32(lblNroBoleta.Text);
                    eBoleta.Stock = int.Parse(txtCantidad.Text);
                    MessageBox.Show(lblNroBoleta.Text + "" + cmProducto.Text);
                    string respuesta = eBoleta.Agregar();
                    ListarVenta();



                    // darle a enabled para el cliente y el vendedor 
                    cmCliente.Enabled = false;
                    cmVendedor.Enabled = false;
                    

                    MessageBox.Show(respuesta);
                    




            }


        }

        private void GenerarBoleta_Load(object sender, EventArgs e)
        {
            ListarVenta();
            ListarComboBox();
            ListarBoleta();
            
        }

        private void lblNroBoleta_Click(object sender, EventArgs e)
        {
            
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            decimal respuesta = boleta.CalcularTotalPorBoleta(Convert.ToDecimal(lblNroBoleta.Text));
            lblTotal.Text = Convert.ToString(respuesta);
            lblNroBoleta.Text = NroBoleta().ToString();
        }
            
        }
    }


