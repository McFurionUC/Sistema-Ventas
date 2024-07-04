using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using CapaBaseDatos;


namespace CapaNegocio
{
    public class BoletaBL
    {
        VentasDataContext ventas = new VentasDataContext();

        public List<Detalle> ObtenerDetalles(int? nroBoleta)
        {
            if (nroBoleta.HasValue)
            {
                var listar = from B in ventas.Detalle
                             where B.NroBoleta == nroBoleta
                             select B;
                return listar.ToList();
            }
            else
            {
                throw new InvalidOperationException("No se puede obtener detalles sin un número de boleta específico.");
            }
        }

        public List<Boleta> ObtenerBoletas()
        {
            var listar = from B in ventas.Boleta
                         select B;
            return listar.ToList();
        }

        public Tuple<string, int, decimal> Buscar(int NroBoleta)
        {
            Console.WriteLine(" " + NroBoleta);
            try
            {
                string CantBoletas = " ";
                int cantidad = 0;
                decimal PrecioUnitario = 0;
                // Convertir nroBoletaText a int para comparar con D.NroBoleta
                var boletaEncontrada = (from D in ventas.Detalle
                                        where D.NroBoleta == NroBoleta
                                        select D);

                if (boletaEncontrada != null)
                {
                    // Mostrar los datos de la boleta encontrada en los campos de texto
                    // Calcular el precio final
                    //Como son más de una boleta con el mismo codigo
                    foreach (var boleta in boletaEncontrada)
                    {
                        CantBoletas += $"{boleta.CodProducto}  ";
                        cantidad += boleta.Cantidad ?? 0;
                        PrecioUnitario += boleta.PrecioUnitario ?? 0;

                    }

                    return Tuple.Create(CantBoletas, cantidad, PrecioUnitario);
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar la boleta: " + ex.Message);
                return null;

            }

        }

        public class TBoleta
        {
            public int NroBoleta { get; set; }
            public string CodProducto { get; set; }
            // Otras propiedades de TBoleta...
        }

        public class TDetalle
        {
            public int NroBoleta { get; set; }
            public string CodProducto { get; set; }
            // Otras propiedades de TDetalle...
        }

        public class TProducto
        {
            public string CodProducto { get; set; }
            public string NombreProducto { get; set; }
            public decimal Precio { get; set; }
            // Otras propiedades de TProducto...
        }

        public class ProductoDetalle
        {
            public string CodProducto { get; set; }
            public int NroBoleta { get; set; }
            public string NombreProducto { get; set; }
            public decimal Precio { get; set; }
        }


        // Generar datos para el comboBox
        public List<string> ObtenerCliente()
        {
            var clientes = (from C in ventas.Cliente
                            select C.CodCliente).Distinct().ToList();

            return clientes;
        }
        public List<string> ObtenerVendedor()
        {
            var vendedor = (from V in ventas.Vendedor
                            select V.CodVendedor).Distinct().ToList();

            return vendedor;
        }
        public List<string> ObtenerCategoria()
        {
            var categorias = (from P in ventas.Producto
                              select P.CodCategoria).Distinct().ToList();

            return categorias;
        }
        public List<string> ObtenerProducto()
        {
            var categorias = (from P in ventas.Producto
                              select P.CodProduto).Distinct().ToList();

            return categorias;
        }
        public int ObtenerSiguienteNumeroBoleta()
        {
            // Consultar el último número de boleta
            var ultimoNumero = ventas.Boleta.Max(b => b.NroBoleta);

            // Retornar el siguiente número de boleta
            return ultimoNumero + 1;
        }
        public string AgregarProducto(string CodCliente, string CodProducto, string CodVendedor, int NroBoleta, int Stock)
        {
            // Insertar en Boleta
            Boleta AgregarVenta = new Boleta();
            AgregarVenta.NroBoleta = NroBoleta;
            AgregarVenta.CodCliente = CodCliente;
            AgregarVenta.CodVendedor = CodVendedor;
            AgregarVenta.Fecha = DateTime.Now; // Fecha actual
            AgregarVenta.Anulado = false;

            // Seleccionar el precio del producto
            var P = (from p in ventas.Producto
                     where p.CodProduto == CodProducto
                     select p).FirstOrDefault();

            if (P == null)
            {
                return "Producto no encontrado";
            }

            decimal precio = P.Precio ?? 0m; // Obtener el precio del producto

            // Insertar Detalle
            Detalle AgregarDetalle = new Detalle();
            AgregarDetalle.CodProducto = CodProducto;
            AgregarDetalle.NroBoleta = NroBoleta;
            AgregarDetalle.Cantidad = Stock;
            AgregarDetalle.PrecioUnitario = precio;

            // Agregar detalle a la tabla de detalles y boleta a la tabla de boletas
            ventas.Detalle.InsertOnSubmit(AgregarDetalle);
            ventas.Boleta.InsertOnSubmit(AgregarVenta);

            try
            {
                ventas.SubmitChanges(); // Confirmar la transacción
                return "Agregado exitoso";
            }
            catch (Exception ex)
            {
                // En caso de error, intentar revertir la inserción del detalle
                
                ventas.Detalle.DeleteOnSubmit(AgregarDetalle);
                try
                {
                    ventas.SubmitChanges(); // Confirmar la transacción para eliminar el detalle
                }
                catch (Exception exe)
                {
                    return "Error al agregar el detalle: " + exe.Message+" "+CodProducto+""+NroBoleta;
                }

                return "Error al agregar la boleta: " + ex.Message;
            }

        }
        public List<ProductoDetalle> ObtenerDetalleProductos()
        {
            using (var context = new VentasDataContext())
            {
                var query = from boleta in context.Boleta
                            join detalle in context.Detalle on boleta.NroBoleta equals detalle.NroBoleta
                            join producto in context.Producto on detalle.CodProducto equals producto.CodProduto
                            
                            select new ProductoDetalle
                            {
                                CodProducto = producto.CodProduto,
                                NroBoleta = boleta.NroBoleta,
                                NombreProducto = producto.Nombre,
                                Precio = producto.Precio ?? 0 // Asegúrate de manejar el nullable correctamente
                            };

                return query.ToList();
            }
        }
        public List<ProductoDetalle> ObtenerDetalleVenta(int numero)
        {
            using (var context = new VentasDataContext())
            {
                var query = from boleta in context.Boleta
                            join detalle in context.Detalle on boleta.NroBoleta equals detalle.NroBoleta
                            join producto in context.Producto on detalle.CodProducto equals producto.CodProduto
                            where boleta.NroBoleta==numero
                            select new ProductoDetalle
                            {
                                CodProducto = producto.CodProduto,
                                NroBoleta = boleta.NroBoleta,
                                NombreProducto = producto.Nombre,
                                Precio = producto.Precio ?? 0 // Asegúrate de manejar el nullable correctamente
                            };

                return query.ToList();
            }
        }
        public decimal CalcularTotalPorBoleta(decimal nroBoleta)
        {
            using (var context = new VentasDataContext())
            {
                // Consulta para obtener los detalles de la boleta específica
                decimal? total = context.Detalle
                     .Where(d => d.NroBoleta == nroBoleta)
                     .Sum(d => (d.PrecioUnitario ?? 0m) * d.Cantidad);

                // Verifica si total tiene valor antes de asignarlo
                decimal totalDecimal = total ?? 0m;

                return totalDecimal;
            }
        }

    }
    }
