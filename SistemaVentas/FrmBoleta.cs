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
    public partial class FrmBoleta : Form
    {
        private Form FormPrincipal;
        public FrmBoleta(Form FrmPrincipal)
        {
            InitializeComponent();
            this.FormPrincipal = FrmPrincipal;
            this.FormClosed += new FormClosedEventHandler(FormBoleta_FrmClosed);
        }
        //Llamar al mapeado relacional a traves de un objeto
        VentasDataContext ventas = new VentasDataContext();


        private void ListarDetalles()
        {
            // Consulta para cargar los detalles en el DataGridView
            var detalles = from D in ventas.Detalle
                           select new
                           {
                               D.NroBoleta,
                               D.CodProducto,
                               D.Cantidad,
                               D.PrecioUnitario
                           };

            // Asignar la lista de detalles al DataGridView
            dgvDetalle.DataSource = detalles.ToList();
        }
        private void codigo_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            string nroBoletaText = txtNroBoleta.Text.Trim();

            if (string.IsNullOrWhiteSpace(nroBoletaText))
            {
                MessageBox.Show("Por favor, ingrese un número de boleta.");
                return;
            }

            try
            {
                // Convertir nroBoletaText a int para comparar con D.NroBoleta
                if (int.TryParse(nroBoletaText, out int nroBoleta))
                {
                    var boletaEncontrada = (from D in ventas.Detalle
                                            where D.NroBoleta == nroBoleta
                                            select D).FirstOrDefault();

                    if (boletaEncontrada != null)
                    {
                        // Mostrar los datos de la boleta encontrada en los campos de texto
                        txtCodProducto.Text = boletaEncontrada.CodProducto;
                        txtCantidad.Text = boletaEncontrada.Cantidad.ToString();
                        txtPrecioUnitario.Text = boletaEncontrada.PrecioUnitario.ToString();

                        // Calcular el precio final
                        decimal cantidad = boletaEncontrada.Cantidad ?? 0; // Convertir int? a decimal
                        decimal precioUnitario = boletaEncontrada.PrecioUnitario ?? 0; // Convertir int? a decimal
                        decimal precioFinal = cantidad * precioUnitario;

                        // Mostrar el precio final en el TextBox correspondiente
                        txtPrecioFinal.Text = precioFinal.ToString();

                        // Opcional: Mostrar solo el resultado en el DataGridView
                        dgvDetalle.DataSource = new[] { boletaEncontrada };
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la boleta con el número especificado.");
                    }
                }
                else
                {
                    MessageBox.Show("El número de boleta ingresado no es válido.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar la boleta: " + ex.Message);
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
    }
}
