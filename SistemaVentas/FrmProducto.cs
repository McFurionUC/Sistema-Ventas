using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using CapaBaseDatos;
using CapaNegocio;
using CapaEntidad;


namespace SistemaVentas
{
    public partial class FrmProducto : Form
    {
        private Form FormPrincipal;
        VentasDataContext ventas = new VentasDataContext();
        ProductoBL Producto = new ProductoBL();
        EProducto eProducto = new EProducto();
        public FrmProducto(Form formPrincipal)
        {
            InitializeComponent();
            ListarProducto();
            ListarComboBox();


            this.FormPrincipal = formPrincipal;
            this.FormClosed += new FormClosedEventHandler(FormPrincipal_FrmClosed);
        }
        private void FormPrincipal_FrmClosed(object sender, FormClosedEventArgs e)
        {
            FormPrincipal.Show();
        }

        private void ListarComboBox()
        {
            List<string> categorias = eProducto.ObtenerCategoria();

            // Asignar las categorías al ComboBox
            cmCodCategoria.DataSource = categorias;

            List<string> unidades = eProducto.ObtenerUnidadMedida();

            // Asignar las categorías al ComboBox
            cmUnidadMedida.DataSource = unidades;
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            ListarProducto();


        }
        public void ListarProducto()
        {

            dgvProducto.DataSource = Producto.Listar();
        }

        private void BindDropDownList()
        { //Selecion de categorias y unidades de medida

            var categorias = ventas.Producto
                .Select(p => p.CodCategoria)
                .Distinct();


            cmCodCategoria.DataSource = categorias;

            var UnidadMedida = ventas.Producto
                .Select(p => p.UnidadDeMedida)
                .Distinct();


            cmUnidadMedida.DataSource = UnidadMedida;


        }

        private void cmCodCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal precio = decimal.Parse(txtPrecio.Text);
            if (string.IsNullOrWhiteSpace(txtNombreProducto.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text) ||
                string.IsNullOrWhiteSpace(cmUnidadMedida.Text) ||
                string.IsNullOrWhiteSpace(cmCodCategoria.Text)
                )
            {
                MessageBox.Show("Todos los campos deben ser llenados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si algún campo está vacío
            }
            else
            {

                eProducto.CodProducto = txtCodProducto.Text;
                eProducto.Nombre = txtNombreProducto.Text;
                eProducto.CodCategoria = cmCodCategoria.Text;
                eProducto.Precio = precio;
                eProducto.Stock = int.Parse(txtStock.Text);
                eProducto.UnidadMedida = cmUnidadMedida.Text;
                string respuesta = eProducto.Agregar();
                ListarProducto();

                MessageBox.Show(respuesta);
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            
            decimal precio = decimal.Parse(txtPrecio.Text);
            if (string.IsNullOrWhiteSpace(txtNombreProducto.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text) ||
                string.IsNullOrWhiteSpace(cmUnidadMedida.Text) ||
                string.IsNullOrWhiteSpace(cmCodCategoria.Text)
                )
            {
                MessageBox.Show("Todos los campos deben ser llenados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si algún campo está vacío
            }
            else
            {

                eProducto.CodProducto = txtCodProducto.Text;
                eProducto.Nombre = txtNombreProducto.Text;
                eProducto.CodCategoria = cmCodCategoria.Text;
                eProducto.Precio = precio;
                eProducto.Stock = int.Parse(txtStock.Text);
                eProducto.UnidadMedida = cmUnidadMedida.Text;
                string respuesta = eProducto.Actualizar();
                ListarProducto();

                MessageBox.Show(respuesta);
            }
            

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Obtener el código del cliente a eliminar
            if (string.IsNullOrWhiteSpace(txtCodProducto.Text))

            {
                MessageBox.Show("Tienes que llenar el codigo del cliente para poder eliminarlo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Salir del método si algún campo está vacío
            }
            else
            {
                eProducto.CodProducto = txtCodProducto.Text.Trim();
                string respuesta = eProducto.Eliminar();
                ListarProducto();
                MessageBox.Show(respuesta);

            
            }
        }
    }
}
