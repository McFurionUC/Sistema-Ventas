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

namespace SistemaVentas
{
    public partial class FrmProducto : Form
    {
        private Form FormPrincipal;
        public FrmProducto(Form formPrincipal)
        {
            InitializeComponent();
            this.FormPrincipal = formPrincipal;
            this.FormClosed += new FormClosedEventHandler(FormPrincipal_FrmClosed);
        }
        private void FormPrincipal_FrmClosed(object sender, FormClosedEventArgs e)
        {
            FormPrincipal.Show();
        }

        VentasDataContext ventas = new VentasDataContext();

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            ListarProducto();
            BindDropDownList();
        }
        public void ListarProducto()
        {
            var listar = from P in ventas.Producto
                         select P;
            dgvProducto.DataSource = listar;
        }
        private void BindDropDownList()
        { //Selecion de categorias y unidades de medida

            var categorias = ventas.Producto
                .Select(p => p.CodCategoria)
                .Distinct();
                              
  
            cmCodCategoria.DataSource = categorias;

            var UnidadMedida=ventas.Producto
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
            Producto ProductoAgregar = new Producto();
            ProductoAgregar.CodProduto = txtCodProducto.Text;
            ProductoAgregar.Nombre = txtNombreProducto.Text;
            ProductoAgregar.CodCategoria = cmCodCategoria.Text;
            ProductoAgregar.Precio = precio;
            ProductoAgregar.Stock = int.Parse(txtStock.Text);
            ProductoAgregar.UnidadDeMedida = cmUnidadMedida.Text;
            ventas.Producto.InsertOnSubmit(ProductoAgregar);

            try
            {
                ventas.SubmitChanges(); // Confirma la transacción
                txtCodProducto.Clear(); txtNombreProducto.Clear(); txtStock.Clear(); txtPrecio.Clear();
                ListarProducto();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
