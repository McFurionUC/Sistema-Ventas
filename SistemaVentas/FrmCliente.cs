using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaBaseDatos;
using CapaEntidad;
using CapaNegocio;

namespace SistemaVentas
{
    public partial class FrmCliente : Form
    {
        private Form FormPrincipal;
        // Llamar al mapeado objeto relacional a través de un objeto 
            VentasDataContext ventas = new VentasDataContext();
        ClienteBL cliente = new ClienteBL();
        ECliente eCliente = new ECliente();
        public FrmCliente(Form FrmPrincipal)
        {
            InitializeComponent();
            
            this.FormPrincipal = FrmPrincipal;
            this.FormClosed += new FormClosedEventHandler(FormCliente_FrmClosed);

        }

        

        // Crear procedimiento para listar la tabla Cliente
        public void ListarClientes()
        {
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = cliente.Listar();
        }

       


        private void BtnAgregar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodCliente.Text) ||
            string.IsNullOrWhiteSpace(txtApellidoCliente.Text) ||
            string.IsNullOrWhiteSpace(txtNombreCliente.Text) ||
            string.IsNullOrWhiteSpace(txtDireccionCliente.Text))
            {
                MessageBox.Show("Todos los campos deben ser llenados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si algún campo está vacío
            }
            else
            {
                eCliente.Apellido = txtApellidoCliente.Text.Trim();
                eCliente.Nombre = txtNombreCliente.Text.Trim();
                eCliente.Direccion = txtDireccionCliente.Text.Trim();
                eCliente.CodCliente = txtCodCliente.Text.Trim();
                string respuesta = eCliente.Agregar();
                ListarClientes();
                MessageBox.Show(respuesta);
                

            }
        }




        private void BtnEliminar_Click_1(object sender, EventArgs e)
        {
            // Obtener el código del cliente a eliminar
            if (string.IsNullOrWhiteSpace(txtCodCliente.Text) )
            
            {
                MessageBox.Show("Tienes que llenar el codigo del cliente para poder eliminarlo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si algún campo está vacío
            }
            else
            {
                eCliente.CodCliente = txtCodCliente.Text.Trim();
                string respuesta = eCliente.Eliminar();
                ListarClientes();
                MessageBox.Show(respuesta);
                

            }
        }



        private void BtnActualizar_Click_1(object sender, EventArgs e)
        {
            // Buscar el cliente a actualizar
            if (string.IsNullOrWhiteSpace(txtCodCliente.Text) ||
            string.IsNullOrWhiteSpace(txtApellidoCliente.Text) ||
            string.IsNullOrWhiteSpace(txtNombreCliente.Text) ||
            string.IsNullOrWhiteSpace(txtDireccionCliente.Text))
            {
                MessageBox.Show("Todos los campos deben ser llenados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si algún campo está vacío
            }
            else
            {
                eCliente.Apellido = txtApellidoCliente.Text.Trim();
                eCliente.Nombre = txtNombreCliente.Text.Trim();
                eCliente.Direccion = txtDireccionCliente.Text.Trim();
                eCliente.CodCliente = txtCodCliente.Text.Trim();
                string respuesta = eCliente.Actualizar();
                dgvClientes.DataSource = null;
                ListarClientes();
                MessageBox.Show(respuesta);
                

            }

        }

        private void txtApellidoCliente_TextChanged(object sender, EventArgs e)
        {

        }
        private void FormCliente_FrmClosed(object sender, FormClosedEventArgs e)
        {
            FormPrincipal.Show();
        }

        

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            ListarClientes();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

