using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas
{
    public partial class FrmVendedor : Form
    {
        private Form FormPrincipal;
        public FrmVendedor(Form FrmPrincipal)
        {
            InitializeComponent();
            this.FormPrincipal = FrmPrincipal;
            this.FormClosed += new FormClosedEventHandler(FormPrincipal_FrmClosed);
        }
        private void FormPrincipal_FrmClosed(object sender, FormClosedEventArgs e)
        {
            FormPrincipal.Show();
        }

        //Llamar al mapeado objeto relacional a travez de un objeto 

        VentasDataContext ventas = new VentasDataContext();

        private void Form1_Load(object sender, EventArgs e)
        {
            Listar();
        }
        private void Listar()
        {
            try
            {
                var consulta = from V in ventas.Vendedor
                               select V;
                
                dgvVendedor.DataSource = consulta.ToList(); // Convierte la consulta a una lista y asigna al DataSource del DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            TxtCodVendedor.Clear();
            TxtApellidos.Clear();
            TxtNombre.Clear();
            TxtCodVendedor.Focus();
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            string codigoVendedor = TxtCodVendedor.Text;

            if (string.IsNullOrWhiteSpace(codigoVendedor))
            {
                MessageBox.Show("Por favor, ingrese un código de vendedor.");
                return;
            }

            try
            {
                var vendedorEncontrado = (from V in ventas.Vendedor
                                          where V.CodVendedor == codigoVendedor
                                          select V).FirstOrDefault();

                if (vendedorEncontrado != null)
                {
                    // Mostrar los datos del vendedor en los campos de texto
                    TxtApellidos.Text = vendedorEncontrado.Apellidos;
                    TxtNombre.Text = vendedorEncontrado.Nombres;

                    // Opcional: Si deseas mostrar solo el resultado en el DataGridView
                    dgvVendedor.DataSource = new List<Vendedor> { vendedorEncontrado };
                }
                else
                {
                    MessageBox.Show("No se encontró un vendedor con el código especificado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el vendedor: " + ex.Message);
            }

        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            Vendedor vendedor = new Vendedor();

            vendedor.CodVendedor = TxtCodVendedor.Text;
            vendedor.Apellidos = TxtApellidos.Text;
            vendedor.Nombres = TxtNombre.Text;

            ventas.Vendedor.InsertOnSubmit(vendedor);
            try
            {
                ventas.SubmitChanges();//Confirmar la transaccion

                TxtCodVendedor.Clear(); TxtApellidos.Clear(); TxtNombre.Clear();
                TxtCodVendedor.Focus();
                Listar();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            var CodVendedorActualizar = TxtCodVendedor.Text;

            // Buscar el vendedor en la base de datos
            var vendedorActualizar = (from V in ventas.Vendedor
                                      where V.CodVendedor == CodVendedorActualizar
                                      select V).FirstOrDefault();

            if (vendedorActualizar != null)
            {
                // Actualizar las propiedades del vendedor con los nuevos valores de los campos de texto
                vendedorActualizar.Apellidos = TxtApellidos.Text;
                vendedorActualizar.Nombres = TxtNombre.Text;

                try
                {
                    // Confirmar la transacción para guardar los cambios en la base de datos
                    ventas.SubmitChanges();
                    MessageBox.Show("Datos actualizados correctamente.");

                    // Limpiar los campos de texto y actualizar la lista
                    TxtCodVendedor.Clear();
                    TxtApellidos.Clear();
                    TxtNombre.Clear();
                    TxtCodVendedor.Focus();
                    Listar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No se encontró el vendedor con el código especificado.");
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            try
            {
                var vendedorEliminar = (from V in ventas.Vendedor
                                        where V.CodVendedor == TxtCodVendedor.Text
                                        select V).FirstOrDefault();

                if (vendedorEliminar != null)
                {
                    ventas.Vendedor.DeleteOnSubmit(vendedorEliminar);
                    ventas.SubmitChanges();
                    LimpiarCampos(); // Limpia los campos después de eliminar
                    Listar();
                }
                else
                {
                    MessageBox.Show("No se encontró el vendedor con el código especificado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar vendedor: " + ex.Message);
            }
        }

        private void boletaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBoleta frmBoleta = new FrmBoleta(this);
            frmBoleta.Show();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCliente frmCliente = new FrmCliente(this);
            frmCliente.Show();
        }
    }
}
