using CapaBaseDatos;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EProducto
    {
        ProductoBL producto = new ProductoBL();
        
        public string CodProducto { get; set; }
        public string Nombre  { get; set; }
        public string UnidadMedida { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string CodCategoria { get; set; }

        public string Agregar()
        {
           return  producto.Agregar(CodProducto, Nombre,UnidadMedida,Precio,Stock,CodCategoria);
        }
        public string Eliminar()
        {
            return producto.Eliminar(CodProducto);
        }
        public string Actualizar()
        {

            return producto.Actualizar(CodProducto, Nombre, UnidadMedida, Precio, Stock, CodCategoria);

        }
        public List<string> ObtenerCategoria()
        {
            return producto.ObtenerCategorias();
        }
        public List<string> ObtenerUnidadMedida()
        {
            return producto.ObtenerUnidadMedida();
        }



    }
}
