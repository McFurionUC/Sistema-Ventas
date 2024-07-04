using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using CapaBaseDatos;

namespace CapaNegocio
{
    public class ProductoBL
    {
        VentasDataContext ventas = new VentasDataContext();
        public List<Producto> Listar()
        {
            var listar = from P in ventas.Producto
                         select P;
            return listar.ToList();

        }
        public List<string> ObtenerCategorias()
        {
            var categorias = (from P in ventas.Producto
                              select P.CodCategoria).Distinct().ToList();

            return categorias;
        }
        public List<string> ObtenerUnidadMedida()
        {
            var categorias = (from P in ventas.Producto
                              select P.UnidadDeMedida).Distinct().ToList();

            return categorias;
        }
        public string Agregar(string CodProducto, string Nombre, string UnidadMedida, decimal Precio, int Stock, string CodCategoria)
        {
            Producto ProductoAgregar = new Producto();

            ProductoAgregar.CodProduto = CodProducto;
            ProductoAgregar.Nombre = Nombre;
            ProductoAgregar.UnidadDeMedida = UnidadMedida;
            ProductoAgregar.Precio = Precio;
            ProductoAgregar.Stock = Stock;
            ProductoAgregar.CodCategoria = CodCategoria;
            ventas.Producto.InsertOnSubmit(ProductoAgregar);
            try
            {
                ventas.SubmitChanges(); // Confirma la transacción
                return "Agregado exitoso";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string Actualizar(string CodProducto, string Nombre, string UnidadMedida, decimal Precio, int Stock, string CodCategoria)
        {

            var ProductoActualizar = (from P in ventas.Producto
                                      where P.CodProduto.Contains(CodProducto)
                                      select P).First();

            if (ProductoActualizar != null)
            {
                ProductoActualizar.Nombre = Nombre;
                ProductoActualizar.UnidadDeMedida = UnidadMedida;
                ProductoActualizar.Precio = Precio;
                ProductoActualizar.Stock = Stock;
                ProductoActualizar.CodCategoria = CodCategoria;

                try
                {
                    ventas.SubmitChanges(); // Confirma la transacción
                    return "Actualización exitosa";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return "Producto no encontrado";
            }



        }
        public string Eliminar(string CodProducto)
        {
            var EliminarProducto = (from P in ventas.Producto
                                    where P.CodProduto.Contains(CodProducto)
                                    select P).First();

            ventas.Producto.DeleteOnSubmit(EliminarProducto);
            try
            {
                ventas.SubmitChanges(); // Confirma la eliminación
                return "Eliminacion Correcta";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
    }
}
