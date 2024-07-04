using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaNegocio;

namespace CapaEntidad
{
    public class ECliente
    {
        ClienteBL cliente = new ClienteBL();
        public string CodCliente { set; get; }
        public string Apellido { set; get; }
        public string Nombre { set; get; }
        public string Direccion { set; get; }


        public string Agregar()
        {

            return cliente.Agregar(CodCliente, Apellido, Nombre, Direccion);
            
        }
        public string Eliminar()
        {
            return cliente.Eliminar(CodCliente);
        }
        public string Actualizar()
        {

            return cliente.Actualizar(CodCliente, Apellido, Nombre, Direccion);

        }


    }
}
